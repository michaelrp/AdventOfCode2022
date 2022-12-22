# Day 15

Part 2 was tricky for me. I tried the method that was an extension of Part 1, but I was dubious: making a 4000000 x 4000000 grid and fill it in, then look for the one spot that is still empty? Not going to work with 1.6e+13 possibilities.

After trying various "enhancements" (e.g., examine each row individually) that were always too slow, I took a look at what [betaveros](https://github.com/betaveros/advent-of-code-2022/blob/main/p15.noul) came up with and saw this comment: "Basically since the point we're looking for is unique, it has to be at the intersection of the boundaries of squares." Ah!

Instead of using the obviously superior and faster method of looking for intersections, I implemented something similar. It finds all the points just outside of each sensor's range, and keeps track of the number of sensor that are adjacent. Then find the point (or points) with at least four edges. Just one!

This does not run in 15 seconds. More like 40 seconds on my older Macbook.
