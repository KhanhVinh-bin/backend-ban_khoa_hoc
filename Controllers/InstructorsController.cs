using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Du_An_Web_Ban_Khoa_Hoc.Models;
using Du_An_Web_Ban_Khoa_Hoc.Models.Data;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Plugins;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Net;
using Du_An_Web_Ban_Khoa_Hoc.Models.DTO;

namespace Du_An_Web_Ban_Khoa_Hoc.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class InstructorsController : Controller
    {
        private readonly AppDbContext _context;

        public InstructorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Instructors/{id}
        // Get: Lấy hồ sơ Giảng Viên bằng {id}
        [HttpGet("Lấy hồ sơ Giảng Viên bằng {id}")]
        public async Task<IActionResult> GetID(int id)
        {
            var profile = await _context.Instructors
                .Where(i => i.InstructorId == id)
            .Select(i => new
            {
                    i.InstructorId,
                    i.Expertise,
                    i.Biography,
                    i.ExperienceYears,
                    i.RatingAverage,     
                    i.Certifications,    
                    i.TotalStudents, 
                    i.TotalCourses, 


                    UserFullName = i.InstructorNavigation.FullName,
                    UserEmail = i.InstructorNavigation.Email,
                    UserAvatar = i.InstructorNavigation.AvatarUrl,
                    UserPhoneNumber = i.InstructorNavigation.PhoneNumber,
                    UserAddress = i.InstructorNavigation.Address,
            })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (profile == null) return NotFound();
            return Ok(profile);
        }

        //Put: api/Instructor + User/All
        //Chỉnh sửa toàn bộ hồ sơ Giảng Viên 
        [HttpPut("api/Instructor/{id}")]
        public async Task<IActionResult> PutProfileInstructors(int id, [FromBody] UpdateI_Profile_Instructor dto)
        {
            var inst = await _context.Instructors
                .Include(i => i.InstructorNavigation)
                .FirstOrDefaultAsync(i => i.InstructorId == id);

            if (inst == null) return NotFound();

            // update instructor fields
            inst.Expertise = dto.Expertise ?? inst.Expertise;
            inst.Biography = dto.Biography ?? inst.Biography;
            inst.ExperienceYears = dto.ExperienceYears ?? inst.ExperienceYears;
            inst.Education = dto.Education ?? inst.Education;
            inst.Certifications = dto.Certifications ?? inst.Certifications;

            // update user fields
            if (inst.InstructorNavigation != null)
            {
                inst.InstructorNavigation.FullName = dto.FullName ?? inst.InstructorNavigation.FullName;
                inst.InstructorNavigation.Email = dto.Email ?? inst.InstructorNavigation.Email;
                inst.InstructorNavigation.PhoneNumber = dto.PhoneNumber ?? inst.InstructorNavigation.PhoneNumber;
                inst.InstructorNavigation.Address = dto.Address ?? inst.InstructorNavigation.Address;
                inst.InstructorNavigation.AvatarUrl = dto.AvatarUrl ?? inst.InstructorNavigation.AvatarUrl;
                inst.InstructorNavigation.Bio = dto.Bio ?? inst.InstructorNavigation.Bio;
                inst.InstructorNavigation.Gender = dto.Gender ?? inst.InstructorNavigation.Gender;
                // DateOfBirth: cần check null vì DateOnly khác DateTime
                inst.InstructorNavigation.DateOfBirth = dto.DateOfBirth ?? inst.InstructorNavigation.DateOfBirth;

                // PasswordHash: thông thường không update chung với profile, 
                // mà nên có API riêng (ChangePassword) → tránh rủi ro bảo mật
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}
