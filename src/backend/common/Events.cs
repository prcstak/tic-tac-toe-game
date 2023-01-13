namespace common;

public record GameEndedEvent(string LeftPlayer, string RightPlayer, bool leftWon);