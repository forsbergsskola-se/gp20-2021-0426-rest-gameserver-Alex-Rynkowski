using System;

namespace MMORPG.Data{
    public class NewQuest{
        readonly Quest quest;

        public NewQuest(string questName, int levelRequirement){
            this.quest = new Quest{
                QuestId = Guid.NewGuid(),
                QuestName = questName,
                LevelRequirement = levelRequirement
            };
            CalculateExpReward(levelRequirement);
            GetReward();
        }

        void CalculateExpReward(int levelRequirement){
            this.quest.ExpReward = new Random().Next(levelRequirement * 30, levelRequirement * 80);
        }

        void GetReward(){
            this.quest.GoldReward =
                new Random().Next(this.quest.LevelRequirement * 20, this.quest.LevelRequirement * 60);
        }

        public Quest SetupNewQuest()
            => this.quest;
    }
}