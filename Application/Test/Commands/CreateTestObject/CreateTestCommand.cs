//using CSharpFunctionalExtensions;

//using Domain.Abstractions;
//using Domain.Entities.Test;

//namespace Application.Test.Commands.CreateTestObject;
//public sealed record CreateTestCommand(string Something) : ICommand<TestObjectId>;

//internal sealed class CreateTestCommandHandler(ITestCommandRepository repository, IUnitOfWork unitOfWork)
//    : ICommandHandler<CreateTestCommand, TestObjectId>
//{
//    public async Task<Maybe<TestObjectId>> Handle(CreateTestCommand command, CancellationToken cancellationToken)
//    {
//        var someNewObject = new TestObject("this is a test");
//        var result = await Result.Try(async () =>
//        {
//            await repository.Add(someNewObject);

//            await unitOfWork.SaveChangesAsync(cancellationToken);
//        });


//        return Result.;
//    }
//}

//internal interface ICommandHandler<TCommand, TId>
//{
//}

//internal interface ITestCommandRepository
//{
//    public Task<TestObjectId> Add(TestObject entity);
//}

//public interface ICommand<T>
//{
//}