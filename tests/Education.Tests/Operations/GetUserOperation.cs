namespace Education.Tests.Operations
{
    public class GetUserOperation
    {
        private readonly IGetPersonNameOperation getPersonNameOperation;
        private readonly IGetPersonSurnameOperation getPersonSurnameOperation;

        public GetUserOperation(IGetPersonNameOperation getPersonNameOperation, IGetPersonSurnameOperation getPersonSurnameOperation) {
            this.getPersonNameOperation = getPersonNameOperation;
            this.getPersonSurnameOperation = getPersonSurnameOperation;
        }

        public (string, string) GetUserData() => (getPersonNameOperation.Execute(), getPersonSurnameOperation.Execute());

        public string GetName() => getPersonNameOperation.Execute();

        public string GetSurname() => getPersonSurnameOperation.Execute();
    }

    public interface IGetPersonNameOperation
    {
        string Execute();
    }

    public interface IGetPersonSurnameOperation
    {
        string Execute();
    }
}
