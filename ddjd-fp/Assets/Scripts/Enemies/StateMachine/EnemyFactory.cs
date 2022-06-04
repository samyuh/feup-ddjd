public class EnemyFactory {
    public SpiderDeath SpiderDeath;
    public SpiderAttack SpiderAttack;
    public SpiderWalk SpiderWalk;

    public EnemyFactory(SpiderController context, EnemyStateMachine stateMachine) {
        SpiderAttack = new SpiderAttack(context, stateMachine, this);
        SpiderWalk = new SpiderWalk(context, stateMachine, this);
        SpiderDeath = new SpiderDeath(context, stateMachine, this);
    }
}