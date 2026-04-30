public interface IActionVisitor
{
    public abstract void Visit(Player player);
    public abstract void Visit(Boulder boulder);
}