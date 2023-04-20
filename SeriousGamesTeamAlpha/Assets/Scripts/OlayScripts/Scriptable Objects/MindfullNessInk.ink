VAR name = "MMGirl"
VAR HC = "red"
Hi There!

#speaker: {name} #emotion: neutral
My name is {name} and welcome to the <color={HC}> Mindfulness Programme! </color>

->main

===main===
Here we will do plenty of <color={HC}>breathing</color> and <color={HC}>physical exercises</color>  

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

-> END