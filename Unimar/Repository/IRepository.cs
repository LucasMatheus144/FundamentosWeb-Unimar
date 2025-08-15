namespace Unimar.Console.Repository
{
    public interface IRepository
    {
        void Incluir(object model);

        void Salvar(object model);

        void Excluir(object model);

        T BuscarPorId<T>(int id) where T : class;

        IQueryable<T> BuscarTodos<T>() where T : class;


    }
}
