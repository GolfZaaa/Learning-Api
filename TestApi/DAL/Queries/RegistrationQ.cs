using Microsoft.EntityFrameworkCore;
using TestApi.DAL.Response;
using TestApi.Models;

namespace TestApi.DAL.Queries
{
    public class RegistrationQ
    {
        private readonly KRU_MIS_Context _context = new KRU_MIS_Context();

        public async Task<ResponsePagination> GetRegistration(int pageSize, int currentPage, string search)
        {
            var registration = await _context.Registration.Where(p => p.Name.Contains(search)).OrderBy(p => p.Name).ToListAsync();

            int totalRow = 0;
            totalRow = registration.Count();
            var totalPage = (double)totalRow / pageSize;
            var value = registration.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            totalPage = (int)Math.Ceiling(totalPage);

            return new ResponsePagination
            {
                StatusCode = 200,
                TaskStatus = true,
                Message = "Success",
                Pagin = new
                {
                    Currentpage = currentPage,
                    Pagesize = pageSize,
                    Totalrows = totalRow,
                    Totalpages = totalPage
                },
                Data = registration
            };
        }

        //สำหรับลบข้อมูล
        public async Task<ResponseMessages> DeleteRegistration(int id)
        {
            Registration data = await _context.Registration.FirstOrDefaultAsync(p => p.Id == id);
            if (data == null) return new ResponseMessages() { StatusCode = 200, TaskStatus = false, Message = "Code Not Found" };

            data.JoinStatus = "0";

            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ResponseMessages() { StatusCode = 200, TaskStatus = true, Message = "Success" };
        }


        //แก้ไขข้อมูล
        public async Task<ResponseMessages> EditRegistration(int Id, Registration registration)
        {
            Registration data = await _context.Registration.FirstOrDefaultAsync(a => a.Id == Id);
            if (data == null) return new ResponseMessages() { StatusCode = 200, TaskStatus = false, Message = "Code Not Found" };

            data.InitialCode = registration.InitialCode;
            data.Name = registration.Name;
            data.Lastname = registration.Lastname;
            data.JoinStatus = "1";
            data.ProjectCode = registration.ProjectCode;

            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ResponseMessages() { StatusCode = 200, TaskStatus = true, Message = "Success" };
        }

        //สร้างข้อมูล
        public async Task<ResponseMessages> CreateRegistration(Registration registration)
        {
            _context.Add(new Registration()
            {
                InitialCode = registration.InitialCode,
            Name = registration.Name,
            Lastname = registration.Lastname,
            JoinStatus = "1",
            ProjectCode = registration.ProjectCode,
        });
            await _context.SaveChangesAsync();

            return new ResponseMessages() { StatusCode = 200, TaskStatus = true, Message = "Success" };
        }

    }
}
