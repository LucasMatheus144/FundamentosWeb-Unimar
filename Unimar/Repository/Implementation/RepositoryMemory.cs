using Unimar.Console.Interface;

namespace Unimar.Console.Repository.Implementation
{
    public class RepositoryMemory : IRepository
    {
        public static int Identificacao = 11;

        public static Dictionary<string, List<Object>> Dados = new Dictionary<string, List<Object>>();

        // Criar a classe no dicionario.
        private List<object> GetLista(object model)
        {
            var nomeClasse = model.GetType().Name;

            if (!Dados.ContainsKey(nomeClasse))
            {
                Dados[nomeClasse] = new List<object>();
            }

            return Dados[nomeClasse];
        }

        public void Incluir(Object objeto)
        {
            var entidade = objeto as IEntidade;

            Identificacao += 10;
            entidade.Id = Identificacao;

            GetLista(objeto).Add(objeto);
        }

        public void Salvar(Object objeto)
        {
            var entidade = objeto as IEntidade;

            GetLista(objeto)[entidade.Id] = objeto;
        }

        public void Excluir(object model)
        {
            GetLista(model).Remove(model);   
        }

        public T BuscarPorId<T>(int id) where T : class
        {
            var nomeEntidade = typeof(T).Name;

            if (!Dados.ContainsKey(nomeEntidade)) return default;
            var entidade = Dados[nomeEntidade].FirstOrDefault(e => (e as IEntidade).Id == id);

            return (T)entidade;
        }

        public IQueryable<T> BuscarTodos<T>() where T : class
        {
            var nomeEntidade = typeof(T).Name;
            if (!Dados.ContainsKey(nomeEntidade))
            {
                return new List<T>().AsQueryable();
            }

            return Dados[nomeEntidade].Cast<T>().AsQueryable();

        }
    }
}
