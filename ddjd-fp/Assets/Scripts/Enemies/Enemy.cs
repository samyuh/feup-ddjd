public abstract class Enemy{

    private int _hp;

    public Enemy(int hp = 5){
        _hp = hp;
    }

    protected abstract void Attack();

}
