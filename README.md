# NekoBentoSolver

That's right, I started a solver for Neko Bento...because as far as I could tell, (1) one hadn't been started, when I looked and (2) the puzzle I was stuck on (lvl 11 of the 3rd expansion pack) was annoying enough (and I'd been stuck on it for long enough) that I didn't want to do it...but I didn't see a solution to it online.

er go, a solver!

FAQ!  
Q) What, no recursion?! This isn't even a solver! *throws desk*  
A) Yeah...I started the project in a completely naive way, which is kinda the way I do anything.  And like I mentioned elsewhere...first try at a solver.

Q) I notice you're using yield return in GetPermutations.  Just how lightweight IS this thing?  
A) Lemme put it this way: It uses under 20MB of RAM and "pins" (<--joke) the CPU at about 6% (on my laptop with an AMD Ryzen 7 Mobile 5700U).  It was never really my intention for it to be so lightweight, and I'll probably try to make it consume more resources, in time.  

But! If I ToList() the result of GetPermutations() (while solving xpac 3, lvl 11), it never (<--hyperbole...probably) manages to materialize it...so I gotta find some kind of balance...which probably means reducing the number of permutations.  (Or abandoning the GetPermutations() approach.)  

(Seriously though, I let the program run for a few days at one point and it still hadn't materialized it (in a laptop with 40GB RAM)...so let's just call it 'currently intractable' and try something else.  ChatGPT said something about my current approaching taking "...longer than human history" to solve that level.  snarky bastard.)
