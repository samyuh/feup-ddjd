public class EnemyFactory {
    public SpiderAttack SpiderAttack;
    public SpiderWalk SpiderWalk;

    public EnemyFactory(SpiderController context, EnemyStateMachine stateMachine) {
        SpiderAttack = new SpiderAttack(context, stateMachine, this);
        SpiderWalk = new SpiderWalk(context, stateMachine, this);
    }
}