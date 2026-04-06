public enum ArrowID
{
    Basic,     // one swipe to success
    Breakable, // requires multiple swipes to break
    Pressable, // pressed and held for a duration
    Opposite,  // success when opposite swipe is performed
    Hideable,  // hides after shown
}