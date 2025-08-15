namespace Unimar.Console.DtoModels
{
    public class ResultOperation
    {
        public bool Sucess { get; set; }

        public List<MensagemErro> Errors { get; private set; } = new List<MensagemErro>();

        public Object Data { get; set; }

        public T Get<T>() where T : class
        {
            if (Data is T data)
            {
                return data;
            }
            throw new InvalidOperationException("Não achou o modelo ai");
        }

        public static ResultOperation Sucesso(object data)
        {
            return new ResultOperation
            {
                Sucess = true,
                Data = data
            };
        }

        public static ResultOperation Falha(List<MensagemErro> erros)
        {
            return new ResultOperation
            {
                Sucess = false,
                Errors =  erros.Select(e => new MensagemErro(e.Propriedade ?? "Desconhecido", e.Mensagem)).ToList()
            };
        }
  
        public static ResultOperation Falha(string Propriedade, string mensagem)
        {
            return new ResultOperation
            {
                Sucess = false,
                Errors = new List<MensagemErro>
                {
                    new MensagemErro(Propriedade, mensagem)
                }
            };
        }

        public static ResultOperation ErroException(Exception ex)
        {
            return new ResultOperation
            {
                Sucess = false,
                Errors = new List<MensagemErro>
                {
                    new MensagemErro("Exception List", ex.Message)
                }
            };
        }
    }


    public class MensagemErro
    {
        public string Propriedade { get; set; }

        public string Mensagem { get; set; }

        public MensagemErro(string propriedade, string mensagem)
        {
            Propriedade = propriedade;
            Mensagem = mensagem;
        }
    }
}
