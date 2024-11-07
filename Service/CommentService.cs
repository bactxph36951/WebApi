using AutoMapper;
using Datas.Entities;
using Dtos.Comments;
using Dtos.Results;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAll();
        Task<CommentResult> GetById(int id);
        Task<CommentResult> Add(CommentCreateRequest request);
        Task<CommentResult> Delete(int id);
        Task<CommentResult> Update(int id, CommentUpdateRequest request);
    }

    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IRepository<Comment> commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentResult> Add(CommentCreateRequest request)
        {
            var validatorparam = new CommentCreateRequestValidator();
            var validateParamResult = await validatorparam.ValidateAsync(request);

            if (!validateParamResult.IsValid)
            {
                return new CommentResult()
                {
                    Errors = validateParamResult.Errors.Select(e=>e.ErrorMessage).ToList(),
                };
            }

            var comment = new Comment()
            {
                Description = request.Description,
                ProductId = request.ProductId,
            };

            await _commentRepository.Add(comment);

            var rs = _mapper.Map<CommentDto>(comment);

            return new CommentResult()
            {
                Comment = rs
            };
        }

        public async Task<CommentResult> Delete(int id)
        {
            var comment = await _commentRepository.Query()
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return new CommentResult()
                {
                    Error = "Comment ko tồn tại"
                };
            }

            await _commentRepository.Delete(comment);

            var rs = _mapper.Map<CommentDto>(comment);

            return new CommentResult()
            {
                Comment = rs,
            };
        }

        public async Task<List<CommentDto>> GetAll()
        {
            var products = await _commentRepository.Query()
                .Include(c=>c.Product)
                .ToListAsync();

            var rs = _mapper.Map<List<CommentDto>>(products);

            return rs;
        }

        public async Task<CommentResult> GetById(int id)
        {
            var comment = await _commentRepository.Query()
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c=>c.Id==id);

            if (comment == null)
            {
                return new CommentResult()
                {
                    Error = "Comment ko tồn tại"
                };
            }

            var rs = _mapper.Map<CommentDto>(comment);

            return new CommentResult()
            {
                Comment = rs,
            };
        }

        public async Task<CommentResult> Update(int id, CommentUpdateRequest request)
        {
            var validatorparam = new CommentUpdateRequestValidator();
            var validateParamResult = await validatorparam.ValidateAsync(request);

            if (!validateParamResult.IsValid)
            {
                return new CommentResult()
                {
                    Errors = validateParamResult.Errors.Select(e => e.ErrorMessage).ToList(),
                };
            }

            var comment = await _commentRepository.Query()
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return new CommentResult()
                {
                    Error = "Comment ko tồn tại"
                };
            }

            comment.Description = request.Description;

            await _commentRepository.Update(comment);

            var rs = _mapper.Map<CommentDto>(comment);

            return new CommentResult()
            {
                Comment = rs,
            };
        }
    }
}
