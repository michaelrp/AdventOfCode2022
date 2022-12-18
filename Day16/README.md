# Day 16

## Part 1

Although it _feels_ wrong, I brute forced this one. Calculate total release for all possible paths visiting valves with `Rate > 0` and picking the biggest.

The winning route (does not include valves where `rate == 0`): `AA->OV->FJ->EL->ST->PF->MD`

Also, started trimming results to improve performance. Added the following:

```csharp
if (remain < 10 && potential / mostRelased.Released < .5)
    continue;
```

This hack reduced the number of evaluations from 231,247 to 69,542.

## Part 2

Yeah, the method I used is clearly not the most efficient. It found the result, but it took a few minutes to test all the 6,000+ combinations of valves. I'm not a fan of this approach.

Onward!
