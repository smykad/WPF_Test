namespace WPF_Test.Models
{
    public interface IBattle
    {
        int SkillLevel { get; set; }
        BattleModeName BattleMode { get; set; }

        int Attack();
        int Defend();
        int Retreat();
    }
}