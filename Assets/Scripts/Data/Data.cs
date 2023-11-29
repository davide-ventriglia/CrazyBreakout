using System.Collections.Generic;


[System.Serializable]
public class AllLeaderboardData{
    public LeaderboardData ClassicLeaderboardData;
    public LeaderboardData CrazyLeaderboardData;
}

[System.Serializable]
public class LeaderboardData{
    public List<User> userDetailsList;
}

[System.Serializable]
public class User{
    public string name;
    public int score;
}
