using UnityEngine;

public class ScoreCounter : MonoBehaviour{
   public int CurrentScore = 0;

   public void AddScore(int count){
      CurrentScore += count;
   }

   public void ResetScore(){
      CurrentScore = 0;
   }
}
