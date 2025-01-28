using AutoMapper;
using DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;
using Services.Dtos;
using Services.Interfaces;
using System.Web;

namespace Presentation.Controllers
{
    public class MeetingRoomController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IMeetingRoomManagementService _meetingRoomManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger<MeetingRoomController> _logger;

        public MeetingRoomController(IFileUploadService fileUploadService,
            IMeetingRoomManagementService meetingRoomRepository,
            IMapper mapper,
            ILogger<MeetingRoomController> logger)
        {
            _fileUploadService = fileUploadService;
            _meetingRoomManagementService = meetingRoomRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = new MeetingRoomCreateModel();
            ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingRoomCreateModel model)
        {
            if (ModelState.IsValid)
            {
                string? imagePath = null;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    try
                    {
                        var imageUploadDto = new FileUploadDto
                        {
                            FileContent = await ConvertToByteArrayAsync(model.ImageFile),
                            FileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName).ToLower(),
                            ContentType = model.ImageFile.ContentType
                        };

                        var imageFolderPath = Path.Combine("wwwroot", "meetingImages");
                        imagePath = await _fileUploadService.UploadFileAsync(imageUploadDto, imageFolderPath);

                        model.ImagePath = imagePath;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Image upload failed");
                        ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
                        ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });

                        TempData.Put("ResponseMessage", new ResponseModel
                        {
                            Message = "Image upload failed",
                            Type = ResponseTypes.Danger
                        });

                        return View(model);
                    }
                }

                var meetingRoom = _mapper.Map<MeetingRoom>(model);

                try
                {
                    await _meetingRoomManagementService.AddMeetingAsync(meetingRoom);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Meeting room created successfully",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "An error occurred while creating the meeting room",
                        Type = ResponseTypes.Danger
                    });

                    _logger.LogError(ex, "An error occurred while creating the meeting room.");
                }
            }

            ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });

            return View(model);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var meetingRoom = await _meetingRoomManagementService.GetMeetingRoomByIdAsync(id);
            if (meetingRoom == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<MeetingRoomUpdateModel>(meetingRoom);

            ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MeetingRoomUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                string? imagePath = null;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    try
                    {
                        var imageUploadDto = new FileUploadDto
                        {
                            FileContent = await ConvertToByteArrayAsync(model.ImageFile),
                            FileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName).ToLower(),
                            ContentType = model.ImageFile.ContentType
                        };

                        var imageFolderPath = Path.Combine("wwwroot", "meetingImages");
                        imagePath = await _fileUploadService.UploadFileAsync(imageUploadDto, imageFolderPath);

                        model.ImagePath = imagePath;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Image upload failed");

                        TempData.Put("ResponseMessage", new ResponseModel
                        {
                            Message = "Image upload failed",
                            Type = ResponseTypes.Danger
                        });

                        ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
                        ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });
                        return View(model);
                    }
                }

                var meetingRoom = _mapper.Map<MeetingRoom>(model);
                meetingRoom.Image = model.ImagePath;


                try
                {
                    await _meetingRoomManagementService.UpdateMeetingAsync(meetingRoom);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Meeting room updated successfully",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the meeting room.");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "An error occurred while updating the meeting room",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _meetingRoomManagementService.DeleteMeetingRoom(id);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "MeetingRoom deleted successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "MeetingRoom deletion failed. Please try again.",
                    Type = ResponseTypes.Danger
                });
                _logger.LogError(ex, "Error occurred while deleting the MeetingRoom with ID: {Id}.", id);
            }

            return RedirectToAction("Index");
        }

        public JsonResult GetMeetingJsonData([FromBody] EntityListModel model)
        {
            var result = _meetingRoomManagementService.GetMeetingRooms(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression("Name", "Location", "Capacity", "Facilities", "Instructions", "TimeLimit", "Image", "Color"));

            var meetingJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                    HttpUtility.HtmlEncode(record.Image),
                    HttpUtility.HtmlEncode(record.Name),
                    HttpUtility.HtmlEncode(record.Facilities),
                    HttpUtility.HtmlEncode(record.Capacity),
                    HttpUtility.HtmlEncode(record.Color),
                    HttpUtility.HtmlEncode(record.Status),
                    record.Id.ToString()
                        }
                    ).ToArray()
            };

            return Json(meetingJsonData);
        }

        private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

    }
}
