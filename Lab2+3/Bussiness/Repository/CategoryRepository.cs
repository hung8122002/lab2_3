using AutoMapper;
using Lab2_3.Bussiness.DTO;
using Lab2_3.Bussiness.IRepository;
using Lab2_3.DataAccess.Managers;
using Lab2_3.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2_3.Bussiness.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        NorthwindContext _context;
        IMapper _mapper;

        public CategoryRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoryDTO> GetCategories()
        {
            CategoryManager categoryManager = new(_context);
            List<Category> categories = categoryManager.GetCategories();
            List<CategoryDTO> categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
            return categoryDTOs;
        }
    }
}
