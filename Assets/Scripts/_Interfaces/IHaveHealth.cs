public interface IHaveHealth
{
    int health { get; set; }

    void ReceiveDamage(int damage);
}
