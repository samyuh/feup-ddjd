public class SpiderFactory {
    public SpiderDeath SpiderDeath;
    public SpiderAttack SpiderAttack;
    public SpiderWalk SpiderWalk;

    public SpiderFactory(SpiderController context, SpiderStateMachine stateMachine) {
        SpiderAttack = new SpiderAttack(context, stateMachine, this);
        SpiderWalk = new SpiderWalk(context, stateMachine, this);
        SpiderDeath = new SpiderDeath(context, stateMachine, this);
    }
}