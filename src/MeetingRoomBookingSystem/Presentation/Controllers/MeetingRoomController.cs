using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;
using Services.Dtos;
using Services.Interfaces;

namespace Presentation.Controllers
{
    public class MeetingRoomController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IMeetingRoomManagementService _meetingRoomManagementService;
        private readonly ILogger<MeetingRoomController> _logger;

        public MeetingRoomController(IFileUploadService fileUploadService,
            IMeetingRoomManagementService meetingRoomRepository,
            ILogger<MeetingRoomController> logger)
        {
            _fileUploadService = fileUploadService;
            _meetingRoomManagementService = meetingRoomRepository;
            _logger = logger;
        }

        public IActionResult Create()
        {
            var model = new MeetingRoomCreateModel();
            ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5" });
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
                        ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5" });
                        ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });
                        return View(model);
                    }
                }

                var meetingRoom = new MeetingRoom
                {
                    Name = model.Name,
                    Location = model.Location,
                    Capacity = model.Capacity,
                    Facilities = model.Facilities,
                    Instructions = model.Instructions,
                    TimeLimit = model.TimeLimit,
                    Color = model.Color,
                    Image = model.ImagePath,
                    QRCode = model.QRCode
                };

                try
                {
                    await _meetingRoomManagementService.AddMeetingAsync(meetingRoom);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the meeting room.");
                }
            }

            ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5" });
            ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });
            return View(model);
        }
        private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
