using System.Collections.Generic;

// The schema and names of objects must coincide with the ones in the firebase database
// otherwise the get request will return null (CASE SENSITIVE)
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
