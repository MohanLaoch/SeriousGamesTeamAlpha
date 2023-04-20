EXTERNAL openJournal()

VAR name = "MFGirl"
VAR HC = "red"
Hi There!

#speaker: MFGirl #emotion: neutral
My name is {name} and welcome to the <color={HC}> Mindfulness Programme! </color>

->main

===main===
Here we will do plenty of <color={HC}>breathing</color> and <color={HC}>physical exercises</color> as well as some <color={HC}>mindful exercises! </color>
Are you excited for it?

+[Yes]
    Yes! That's the spirit! #emotion: excited
    ->BreathingMain
    
+[No]
    Aww! Try it and see how you feel afterwards #emotion: sad
    ->BreathingMain
    


===BreathingMain==

Let's get started with the first breathing exercise. #emotion: neutral
Firstly, <color={HC}> I want you to inhale for about 5 seconds! and then I want you to exhale for 5 seconds! </color> 

Deep breaths now! #delay: 5

Exhale now! #delay: 5

Thats the breathing done! How do you feel?

+[Great]
    That's good to hear! I'm sure you're well relaxed now! #emotion: excited
    ->StretchingSection
+[Punished]
    You're just not used to the exercises yet! #emotion: sad
    You can try again on your own accord before hitting the continue button #emotion: neutral
    ->StretchingSection

===StretchingSection===
Now let's move onto the <color={HC}>Stretches!</color> #emotion: neutral

We're just going to do a simple <color={HC}> stretch pose and I'm gonna need you to hold onto it for a couple of seconds!</color>

Just follow my pose here! #pose: Stretch_01

Now I want you to <color={HC}>hold this for 10 seconds!</color>

Go! #delay: 10

Release!

That was good. I'm sure your body feels a lot more nicer. Those muscles feeling better already!? #emotion: neutral

+[Absolutely]
    They're about to feel even better after this next pose! #emotion: excited
    -> StretchingSection2
+[Worse]
    They're worse!? #emotion: sad
    Well, they're about to feel even better after this next pose! #emotion: neutral
    -> StretchingSection2

===StretchingSection2===
Now I want you to <color={HC}>copy this pose</color> #pose: Stretch_02
Hold this for <color={HC}> 8 seconds!</color>
Go! #delay: 8

I'm already feeling a lot better. Most of my muscles are already relaxed. I hope yours are better than before! #emotion: neutral

+[They are]
    I'm glad that my programme is working for you! #emotion: excited
    ->StretchingSection3
+[They aren't]
    I got one more chance to make this work for you! #emotion: sad
    ->StretchingSection3
    
    
===StretchingSection3===
Ok this is our last pose! #emotion: neutral

I need you to <color={HC}>hold this pose for 5 seconds! </color> #pose: Stretch_03

Now let's see that stretch! #delay: 5

That's good you can stop it there! #emotion: neutral

How did that go for you? 

+[Really well]
    That's good to hear. I'm glad that stretch went well for you! #emotion: excited
    -> Closing
+[Really Bad]
    That's devastating, You can still redo the stretch again to see if it goes your way this time! #emotion: sad
    ->Closing

===Closing===
That's all for the Stretching exercises. How do you feel about them? #emotion: neutral
+[Good]
    I'm glad to hear that the stretches have worked well for you. With your muscles now rested you can freely move to the next exercise! #emotion: excited
    -> NextClosing
+[Bad]
    I'm sorry about that. You can repeat the exercises on your own accord if you couldn't get it right the first time! If you don't want to, you're free to move onto the next exercise! #emotion: sad
    ->NextClosing
    
===NextClosing===
Now for our final exercise we are going to <color={HC}> write messages into a small diary </color>.

I want you to either <color={HC}> write a list of encouraging words to yourself or you can write down your current thoughts and feelings onto the page</color>!

Whether it's your anxieties or your current struggles, You're free to write down anything as you please, don't be afraid to write it all down!

~openJournal()    

-> END