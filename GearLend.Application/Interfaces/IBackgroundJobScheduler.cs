using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GearLend.Application.Interfaces
{
    public interface IBackgroundJobScheduler
    {
        void Enqueue<T>(Expression<Action<T>> methodCall);
        void Enqueue(Expression<Action> methodCall);
    }
}
