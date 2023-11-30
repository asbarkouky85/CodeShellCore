using CodeShellCore.Data;
using CodeShellCore.Data.Events;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Mapping;
using MassTransit;
using System.Threading.Tasks;

namespace CodeShellCore.MQ.Events
{
    public abstract class CrudEventConsumer<TDto, TEntity, TPrime, TUnit> : Consumer
        where TDto : class, IEntityDto<TPrime>
        where TEntity : class, IEntity<TPrime>
        where TUnit : class, IUnitOfWork
    {
        protected TUnit Unit => Store.GetRequiredService<TUnit>();
        protected IRepository<TEntity> Repository => Unit.GetRepositoryFor<TEntity>();

        public virtual async Task<SubmitResult> Update(TDto dto)
        {
            var entity = Unit.GetRepositoryFor<TEntity>().FindSingle(dto.Id);
            if (entity != null)
            {
                Mapper.Map(dto, entity);
                return await Unit.SaveChangesAsync(throwException: false);
            }
            else
            {
                return await Insert(dto);
            }
        }

        public virtual async Task<SubmitResult> Insert(TDto dto)
        {
            var entity = Mapper.Map<TDto, TEntity>(dto);
            await Repository.InsertAsync(entity);
            return await Unit.SaveChangesAsync(throwException: false);
        }

        public virtual async Task<SubmitResult> Delete(TDto dto)
        {
            Repository.DeleteById(dto.Id);
            return await Unit.SaveChangesAsync(throwException: false);
        }

        public virtual async Task Consume(ConsumeContext<CrudEvent<TDto>> context)
        {
            await ApplyEvent(context.Message);
        }

        protected virtual async Task ApplyEvent(CrudEvent<TDto> message)
        {
            switch (message.Type)
            {
                case ActionType.Add:
                    await ConsumeEvent(message, ev => Insert(ev.Data), message.TenantId);
                    break;
                case ActionType.Update:
                    await ConsumeEvent(message, ev => Update(ev.Data), message.TenantId);
                    break;
                case ActionType.Delete:
                    await ConsumeEvent(message, ev => Delete(ev.Data), message.TenantId);
                    break;
            }
        }

    }
}
