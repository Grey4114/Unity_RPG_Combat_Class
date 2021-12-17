// This a contract between the caller of the contract and the implementor of the contract
// So Scheduler(caller) calls for somthing and  Fighter(implementor) implements it,
// but only if IAction confirms that the conditions of the contract have been fulfilled
namespace RPG.Core
{
    public interface IAction
    {
        void Cancel(); 
        // This is a contract 
        // Any class the uses IAction must have the method that is in this interface
        // Exapmle - For this interface the required method is Cancel() 
    }
}