# Day 17

## Part 1

Fairly straightforward. Move your sprite, check for collisions, stack 'em up and count their height.

Could probably improve performance by checking for collisions at the same time as moving each point, but that speed increase is not going to help with 1,000,000,000,000 rocks in ...

## Part 2

:(

Started by thinking I could find a repeated pattern (say, rocks * jets = 50,455 cycles) and do that once plus the remainder and get the height. Unfortunately, the falling rocks do not line up with the jets. I tested it out to 200,000 rocks and did not find any case (other than the first rock) when `rock == 0` and `jet == 0`.

:)

Ok! Instead what works is to search for matching _patterns_. When you find a good one (that won't allow any falling rocks to slip through), you can "skip" ahead in time to a rock that is close to the end.