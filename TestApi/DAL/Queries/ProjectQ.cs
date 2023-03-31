using Microsoft.EntityFrameworkCore;
using TestApi.DAL.Response;
using TestApi.Models;

namespace TestApi.DAL.Queries
{
    public class ProjectQ
    {
        private readonly KRU_MIS_Context _context = new KRU_MIS_Context();

        public async Task<ResponsePagination> GetProject(int pageSize, int currentPage, string search)
        {
            var project = await _context.Project.Where(p => p.ProjectName.Contains(search)).OrderBy(p => p.ProjectName).ToListAsync();

            int totalRow = 0;
            totalRow = project.Count();
            var totalPage = (double)totalRow / pageSize;
            var value = project.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
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
                Data = project
            };

        }
        //สำหรับลบข้อมูล
        public async Task<ResponseMessages> DeleteProject(string id)
        {
            Project data = await _context.Project.FirstOrDefaultAsync(p => p.Code == id);
            if (data == null) return new ResponseMessages() { StatusCode = 200, TaskStatus = false, Message = "Code Not Found" };

            data.Status = "0";

            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ResponseMessages() { StatusCode = 200, TaskStatus = true, Message = "Success" };
        }

        //แก้ไขข้อมูล
        public async Task<ResponseMessages> EditProject(string code,Project projectDT)
        {
            Project data = await _context.Project.FirstOrDefaultAsync(a => a.Code == code);
            if (data == null) return new ResponseMessages() { StatusCode = 200, TaskStatus = false, Message = "Code Not Found" };

            data.ProjectName = projectDT.ProjectName;
            data.Detail = projectDT.Detail;
            data.Status = "1";
            data.CreatedDate = DateTime.Now;
            data.CreatedBy = "B1";

            _context.Entry(data).State= EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ResponseMessages() { StatusCode = 200, TaskStatus = true, Message = "Success" };
        }

        //สร้างข้อมูล
        public async Task<ResponseMessages> CreateProject (Project projectDT)
        {
            _context.Add(new Project()
            {
                ProjectName = projectDT.ProjectName,
                Detail = projectDT.Detail,
                Status = "1",
                CreatedDate = DateTime.Now,
                CreatedBy = "01",
            });
            await _context.SaveChangesAsync();

            return new ResponseMessages() { StatusCode = 200, TaskStatus = true, Message = "Success" };
        }
    }
}
