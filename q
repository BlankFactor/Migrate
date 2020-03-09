[33mcommit ae6dd76d66f30010e19b64a21c8cfca161e04a90[m[33m ([m[1;36mHEAD -> [m[1;32mmaster[m[33m)[m
Author: UnstableFactor <787121798@qq.com>
Date:   Sun Mar 8 19:55:05 2020 +0800

    feat(*):Build forest scene
    - Build forest scene
    - Add day and night module
    - Add post processing
    - Add background music

[33mcommit f2f255ac9d702a7efd7dd728e8fefcbf1493e360[m
Author: UnstableFactor <787121798@qq.com>
Date:   Sat Feb 22 18:56:03 2020 +0800

    feat(*):Add animator for bird
    - Flying,slowing down were added into animator

[33mcommit 437faf487877be4941261b527ae1860b373bf6c9[m[33m ([m[1;31morigin/master[m[33m)[m
Author: UnstableFactor <787121798@qq.com>
Date:   Fri Feb 14 14:17:07 2020 +0800

    fix(*):fix PlayerController lose gameojbect when leaderbird dead
    - Leaderbird will message GameManager when it's dead

[33mcommit 11551f5e5da9308337322d1e4b237d7877bf851e[m
Author: UnstableFactor <787121798@qq.com>
Date:   Thu Feb 13 16:54:16 2020 +0800

    feat(*):Add Poacher module
    - Poacher can shoot birds when they are in poacher's vision

[33mcommit 8944ad0cf31f699d1f53ed16b3eb3b7319074bf6[m
Author: UnstableFactor <787121798@qq.com>
Date:   Wed Feb 12 17:55:16 2020 +0800

    feat(*):Add CameraManager and complete bird controller
    - Leader bird can invite the teamless bird in landPosition
    - Add ExhaustGas that can make bird in dangerous
    - Camera will be get closer when leader bird take off

[33mcommit 147a48afd4cee0a548ec3449e22f33fe8003fff0[m
Author: UnstableFactor <787121798@qq.com>
Date:   Mon Feb 10 17:06:00 2020 +0800

    feat(*):Complete bird-control module and build gamemanager
    - Bird will dead when energy less than zero
    - Time will be record by worldTimeManager when game starting
    - GameManager will record the count of followers
    - Import 2D Render

[33mcommit f54556b7464f973ed4db8affed8621fd86333d2a[m
Author: UnstableFactor <787121798@qq.com>
Date:   Sun Feb 9 17:48:17 2020 +0800

    feat(*) : complete follow system
    - Followers can change thier speed to match their coordination when they are not in border
    - Followers will match their coordination in any time
    - Followers can change their cluster coordination when they are in actionable area

[33mcommit 516cd687fd37d0d96924b9e53f6de5cd105bbc8d[m
Author: UnstableFactor <787121798@qq.com>
Date:   Sat Feb 8 17:36:51 2020 +0800

    feat(*) most of follower system features has alrealdy done
    - Leader can command followers to do the same action as leader
    - Follower need setable reflection time to execute command
    - Finished flying border trigger and it can be set in <LeaderBird>,but follower can not change there position by theirselve for now

[33mcommit 18f351b084d5f0b98c54a9328552c05704e55fec[m
Author: UnstableFactor <787121798@qq.com>
Date:   Fri Feb 7 18:43:34 2020 +0800

    feat(*): Initial project and build basic module
    - base class <BBird> has alreadly done,now bird can be fly,take off,land
    - LandPoint Checker can be seted in setable and visiable blue rectangle area in PlayerController
    - Height Border can be seted in PlayerController with red line by set border data
