namespace SistemaFinanceiro.Dominio.ValueObjects;

public class AuditChanges
{
    private readonly Dictionary<string, AuditFieldChange> _changes;
    public IReadOnlyDictionary<string, AuditFieldChange> Values => _changes;

    public AuditChanges()
    {
        _changes = new Dictionary<string, AuditFieldChange>();
    }

    public void AddChange(string fieldName, object oldValue, object newValue)
    {
        _changes[fieldName] = new AuditFieldChange(oldValue, newValue);
    }
}
