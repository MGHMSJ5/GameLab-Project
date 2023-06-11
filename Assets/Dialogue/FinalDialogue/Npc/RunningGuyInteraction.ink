-> main

=== main ===
Hey, stop what you're doing out there. #speaker:Natasha #portrait:Natasha #layout:right
I am just taking a jog trough the forest, I don't see the problem. #speaker:Jogger #portrait:RunningGuy #layout:left
    * You know you are not supposed to go off of the defined path. #speaker:Natasha #portrait:Natasha #layout:right
        -> DefinedPath
    * Get back on the path! #speaker:Natasha #portrait:Natasha #layout:right
        -> BackOnPath
    * Ugh, never mind. #speaker:Natasha #portrait:Natasha #layout:right
        -> nevermind
        
=== DefinedPath ===
The paths have a reason for being there. #speaker:Natasha #portrait:Natasha #layout:right
Where does it say that, huh? #speaker:Jogger #portrait:RunningGuy #layout:left
    * Throughout the park it is stated on signs, #speaker:Natasha #portrait:Natasha #layout:right
        -> StatedSigns
    * I am telling you the rules right now. #speaker:Natasha #portrait:Natasha #layout:right
        -> Rules

=== StatedSigns ===
to stick to the paths and not to wander off. #speaker:Natasha #portrait:Natasha #layout:right
Excuse me, princess. But why can't I make my own route? #speaker:Jogger #portrait:RunningGuy #layout:left
    * Because this is a nature reserve. #speaker:Natasha #portrait:Natasha #layout:right
        -> NatureReserve
    * Because this is a bird nest and rest area. #speaker:Natasha #portrait:Natasha #layout:right
        -> BirdArea
        
=== NatureReserve ===
We have many protected plants and animals residing here. #speaker:Natasha #portrait:Natasha #layout:right
Continue running there and I will have to fine you.
Sure, I'll try to stay on the path from now on then. #speaker:Jogger #portrait:RunningGuy #layout:left
I'm not interested in getting a fine.
Thank you for keeping the protected species and your wallet in mind. #speaker:Natasha #portrait:Natasha #layout:right
Yeah, sure, no problem. I'll go jog somewhere else. #speaker:Jogger #portrait:RunningGuy #layout:left #jogger:one
-> END

=== BirdArea ===
Do not disturb the diverse range of species residing there. #speaker:Natasha #portrait:Natasha #layout:right
A bird nest and rest area, here? This close to a publicly open forest? #speaker:Jogger #portrait:RunningGuy #layout:left
I've been to many forests to run and jog, 
but I have never seen a bird nest and rest area this close to visitors.
Yeah, that is true. Still, get out of there! #speaker:Natasha #portrait:Natasha #layout:right
Eh, I think you are messing with me. A rest area here seems very unlikely. #speaker:Jogger #portrait:RunningGuy #layout:left
I am most definitely not. #speaker:Natasha #portrait:Natasha #layout:right
Well, I'm just going to finish my jog, have a nice day! #speaker:Jogger #portrait:RunningGuy #layout:left
-> END

=== Rules ===
-> END
        
=== EurasianOtter ===
It is one of the endangered mammals in the Netherlands. #speaker:Natasha #portrait:Natasha #layout:right
But I read in the news that they were taken off the Red List. #speaker:Jogger #portrait:RunningGuy #layout:left
Eh.. #speaker:Natasha #portrait:Natasha #layout:right
You can't fool me that easily, I read the news from time to time. #speaker:Jogger #portrait:RunningGuy #layout:left
So, I'll just go finish my jog while you get your facts straight.
-> END

=== EuropeanRabbit ===
Most people assume it is not endangered, but actually it is. #speaker:Natasha #portrait:Natasha #layout:right
Oh, I've heard about that. #speaker:Jogger #portrait:RunningGuy #layout:left
So, try to stick to the path from now on. #speaker:Natasha #portrait:Natasha #layout:right
That way you won't disturb the nearby rabbits. 
Also, you will be fined if you continue running there!
Fine, I'll keep the rabbits in mind. #speaker:Jogger #portrait:RunningGuy #layout:left
I definitely do not want to pay a fine after a morning jog. #jogger:one
-> END

=== BackOnPath ===
Come on, you can't be out there. So, get back on the path! #speaker:Natasha #portrait:Natasha #layout:right
Who are you to talk? #speaker:Jogger #portrait:RunningGuy #layout:left
I am the forester in charge of this park, #speaker:Natasha #portrait:Natasha #layout:right
so if you wouldn't mind getting back on the path, that would help me out.
Sure, sure. Why bother though, I am not disturbing the forest now, am I? #speaker:Jogger #portrait:RunningGuy #layout:left
It might not look like it to you, but this park has many endangered species. #speaker:Natasha #portrait:Natasha #layout:right
So, when going off the paths you're disturbing them, 
even if you might not notice it yourself.
What kind of endangered species then, eh? #speaker:Jogger #portrait:RunningGuy #layout:left
    * The Eurasian Otter is a prime example. #speaker:Natasha #portrait:Natasha #layout:right
        -> EurasianOtter
    * The European Rabbit is one of them. #speaker:Natasha #portrait:Natasha #layout:right
        -> EuropeanRabbit

=== nevermind ===
Yeah, you have a nice day too! #speaker:Jogger #portrait:RunningGuy #layout:left
-> END