INCLUDE CharacterData.ink
EXTERNAL openJournal()


Hi There!

#speaker: Olivia
#emotion: MF_Neutral
My name is {name} and welcome to the <color={HC}> Mindfulness Programme! </color>

->main

===main===
Here we will do plenty of <color={HC}>breathing</color> and <color={HC}>physical exercises</color> as well as some <color={HC}>mindful exercises! </color>
Are you excited for it?

+[Yes]
    Yes! That's the spirit! #emotion: MF_Excited
    ->BreathingMain
    
+[No]
    Aww! Try it and see how you feel afterwards. #emotion: MF_Sad
    ->BreathingMain
    


===BreathingMain==

Let's get started with the first breathing exercise. #emotion: MF_Neutral
Firstly, <color={HC}> I want you to breathe in for about 5 seconds! Then I want you to breathe out for another 5 seconds! </color> 

Breathe in! #delay: 5

Breathe out! #delay: 5

That's the breathing done! How do you feel?

+[Great]
    That's good to hear! I'm sure you're well relaxed now. #emotion: MF_Excited
    ->StretchingSection
+[Tired]
    Don't worry, you're just not used to the exercises yet. #emotion: MF_Sad
    You can try again on your own before hitting the continue button! #emotion: MF_Neutral
    ->StretchingSection

===StretchingSection===
Now let's move onto the <color={HC}>Stretches!</color> #emotion: MF_Neutral

We're just going to do a simple <color={HC}> stretch pose and I'm going need you to hold onto it for a few seconds.</color>

Just follow my pose. Like this! #pose: MF_Stretch_01

Now I want you to <color={HC}>hold this for 10 seconds!</color>

Go! #delay: 10

Done!

That was good! I'm sure you feel nice and stretched. Those muscles feeling better already? #emotion: MF_Neutral

+[Absolutely]
    They're about to feel even better after this next stretch! #emotion: MF_Excited
    -> StretchingSection2
+[Worse]
    They're worse!? #emotion: MF_Sad
    Well, they're about to feel even better after this next stretch! #emotion: MF_Neutral
    -> StretchingSection2

===StretchingSection2===
Now I want you to <color={HC}>copy this pose!</color> #pose: MF_Stretch_02
Hold this for <color={HC}> 8 seconds!</color>
Go! #delay: 8

I'm already feeling a lot better. My muscles are relaxed now. I hope yours are too! #emotion: MF_Neutral

+[They are]
    I'm glad that my training programme is working for you! #emotion: MF_Excited
    ->StretchingSection3
+[They aren't]
    Perhaps the next stretch will make them better for you. #emotion: MF_Sad
    ->StretchingSection3
    
    
===StretchingSection3===
Okay. This is our last pose! #emotion: MF_Neutral

I need you to <color={HC}>hold this pose for 5 seconds! </color> #pose: MF_Stretch_03

Now let's see that stretch! #delay: 5

That's good you can stop it there! #emotion: MF_Neutral

How do you feel? 

+[Really Good!]
    That's great to hear! I'm glad that stretch went well for you. #emotion: MF_Excited
    -> Closing
+[Really Bad!]
    That's unfortunate. Feel free to try again in your own time. It gets easier with practice! #emotion: MF_Sad
    ->Closing

===Closing===
That's all for the Stretching exercises. How are you feeling? #emotion: MF_Neutral
+[Good]
    I'm glad to hear that the stretches have worked well for you! With your muscles now rested you can freely move to the next exercise. #emotion: MF_Excited
    -> NextClosing
+[Bad]
    I'm sorry about that. You can try the exercises again if you couldn't get them right the first time. If you don't want to, you're free to move onto the next exercise. #emotion: MF_Sad
    ->NextClosing
    
===NextClosing===
Now for our final exercise we are going to <color={HC}> write messages into a small diary </color>. #emotion: MF_Neutral

I want you to either <color={HC}> write a list of encouraging words to yourself, or you can write down your current thoughts and feelings onto the page</color>!

Whether it's your anxieties or your current struggles, you're free to write down anything as you please, don't be afraid to write it all down!

When you're finished writing in your journal, <color={HC}> Press the Enter Key to submit your journal!</color>

~openJournal()    

-> END
