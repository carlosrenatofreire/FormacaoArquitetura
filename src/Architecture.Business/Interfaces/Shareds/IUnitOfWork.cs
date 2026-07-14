namespace Architecture.Business.Interfaces.Shareds
{
    // Coordena a gravação de todas as alterações pendentes de um caso de uso numa única
    // transação (um único SaveChanges), para os repositórios não fazerem cada um o seu commit
    // isolado e deixarem a base num estado intermédio inconsistente em caso de falha a meio.
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
