using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TubeStar
{
    [Serializable]
    public enum EnglishStrings
    {
        [Description("Fails")]
        Fails,

        [Description("Animal fighting")]
        AnimalFighting,

        [Description("Anime")]
        Anime,

        [Description("Cats")]
        Cats,

        [Description("Comedy")]
        Comedy,

        [Description("Conspiracy theory")]
        ConspiraryTheory,

        [Description("Creepypasta")]
        CreepyPasta,

        [Description("Gaming")]
        Gaming,

        [Description("Shopping haul")]
        ShoppingHaul,

        [Description("How to ...")]
        HowTo,

        [Description("Movies")]
        Movies,

        [Description("Music video")]
        MusicVideo,

        [Description("Non profit")]
        NonProfit,

        [Description("Sports")]
        Sports,

        [Description("The weird side")]
        TheWeirdSide,

        [Description("Technology")]
        Technology,

        [Description("Ukulele cover")]
        UkuleleCover,

        [Description("Vlog")]
        Vlog,

        [Description("OK")]
        Ok,

        [Description("Cancel")]
        Cancel,

        [Description("Something went horribly wrong!\nOr maybe only slightly wrong...\nAnyway, sorry! I couldn't save the game!")]
        SaveFail,

        [Description("Something went horribly wrong!\nOr maybe only slightly wrong...\nAnyway, sorry! I couldn't load the game!")]
        LoadFail,

        [Description("Channel '{0}' has been suspended due to a suspected violation of the terms and conditions!")]
        ChannelSuspended,

        [Description("Your computer has crashed!\n\nAll unreleased videos have been lost!")]
        ComputerCrash,

        [Description("The economy is failing!\n\nYour cost of living has increased!")]
        CostOfLivingIncrease,

        [Description("Your work has been outsourced!\n\nYou're fired!")]
        Fired,

        [Description("Some embarrassing information about you has surfaced!\n\nYou've lost {0} subscribers!")]
        LoseSubscribers,

        [Description("Mmm, yeah, I'm going to have to go ahead and ask you to work overtime.\n\nSo if you could go ahead and do that, that'd be great!")]
        WorkOvertime,

        [Description("Video '{0}' has been suspended due to suspected copyright infringement!")]
        VideoSuspended,

        [Description("You searched through your sofa and found {0} change")]
        SpareChange,

        [Description("You've been promoted at work!\n\nYou earn more money while working longer hours!")]
        Promoted,

        [Description("The robots have risen and taken over the earth!\n\nBow down to your new robot rulers.")]
        RobotRising,

        [Description("You've won {0} in a beauty contest!")]
        BeautyContest,

        [Description("Channel '{0}' has had it's suspension lifted!")]
        ChannelSuspensionLifted,

        [Description("The economy is recovering!\n\nYour cost of living has decreased!")]
        CostOfLivingDecreased,

        [Description("You've been given a scholarship to {0}!")]
        FreeStudy,

        [Description("You've been featured on a website!\n\nYou've gained {0} subscribers!")]
        GainSubscribers,

        [Description("A video '{0}' has had it's suspension lifted!")]
        VideoSuspensionLifted,

        [Description("Random event")]
        RandomEvent,

        [Description("Study 'Video Attribution Technology I'")]
        StudyAudienceAnalysis1,

        [Description("Study 'Video Attribution Technology II'")]
        StudyAudienceAnalysis2,

        [Description("Study 'Post Production I'")]
        StudyPostProduction1,

        [Description("Study 'Post Production II'")]
        StudyPostProduction2,

        [Description("Study 'Post Production III'")]
        StudyPostProduction3,

        [Description("Study 'Production I'")]
        StudyProduction1,

        [Description("Study 'Production II'")]
        StudyProduction2,

        [Description("Study 'Production III'")]
        StudyProduction3,

        [Description("Study 'Quality Analysis'")]
        StudyQualityAnalysis,

        [Description("Mandatory Bowing to Robot Rulers")]
        MandatoryBowToRobotRulers,

        [Description("'{0}' post production")]
        EditVideoTask,

        [Description("Job")]
        Job,

        [Description("'{0}' video shoot")]
        ShootVideoTask,

        [Description("May contain cats")]
        CatsAttribute,

        [Description("Video quality increased slightly")]
        CatsAttributeDescription,

        [Description("Copycat")]
        CopycatAttribute,

        [Description("{0} free initial views")]
        CopycatAttributeDescription,

        [Description("Fanboy bait")]
        FanboyBaitAttribute,

        [Description("If the video quality is over 75%, no current subscribers will unsubscribe")]
        FanboyBaitAttributeDescription,

        [Description("Genre buster")]
        GenreBusterAttribute,

        [Description("Video category is less of a factor in likeability")]
        GenreBusterAttributeDescription,

        [Description("Hypnotic")]
        HypnoticAttribute,

        [Description("Higher chance of subscriptions")]
        HypnoticAttributeDescription,

        [Description("Learning from mistakes")]
        LearnFromMistakesAttribute,

        [Description("Player stats slightly increased at cost of video quality")]
        LearnFromMistakesAttributeDescription,

        [Description("Memetic")]
        MemeticAttribute,

        [Description("Higher chance of sharing")]
        MemeticAttributeDescription,

        [Description("Product Placement")]
        ProductPlacementAttribute,

        [Description("Earn more from advertising per view")]
        ProductPlacementAttributeDescription,

        [Description("Better second time")]
        SecondTimeAttribute,

        [Description("The video can be viewed multiple times")]
        SecondTimeAttributeDescription,

        [Description("So bad it's good")]
        SoBadAttribute,

        [Description("Chance of video being shared if the video is disliked")]
        SoBadAttributeDescription,

        [Description("Crowdfunding")]
        CrowdFundingAttribute,

        [Description("Instead of a subscription you get {0}")]
        CrowdFundingAttributeDescription,

        [Description("Above board")]
        AboveBoardAttribute,

        [Description("The video cannot be suspended")]
        AboveBoardAttributeDescription,

        [Description("Unreleased Videos")]
        UnreleasedVideos,

        [Description("Continue game")]
        ContinueGame,

        [Description("New game")]
        NewGame,

        [Description("Tutorial")]
        Tutorial,

        [Description("Login")]
        Login,

        [Description("Credits")]
        Credits,

        [Description("Language Mods")]
        Mods,

        [Description("What a loser")]
        WhatALoser,

        [Description("You ran out of money!")]
        OutOfMoney,

        [Description("Death")]
        Death,

        [Description("You died fighting the robot menace.\nYour official cause of death is noted as:\n\n'Exhaustion from overeagerness to bow to robot masters.'")]
        RobotDeath,

        [Description("Confirm")]
        Confirm,

        [Description("Leave the current game?")]
        LeaveGame,

        [Description("Overwrite save")]
        OverwriteSave,

        [Description("A save file already exists!\nOverwrite?")]
        SaveExists,

        [Description("Start day")]
        StartDay,

        [Description("Help")]
        Help,

        [Description("Exit")]
        Exit,

        [Description("Save Game")]
        SaveGame,

        [Description("Unhandled Error")]
        UnhandledError,

        [Description("An unknown error has occured!\n\nError report created in the games folder.\n\nClick OK to continue, CANCEL to quit")]
        ExceptionText,

        [Description("Aren't you missing something?")]
        MissingValueHeader,

        [Description("Enter a name")]
        MissingName,

        [Description("Select a category")]
        MissingCategory,

        [Description("Select a channel")]
        MissingChannel,

        [Description("Select a strategy")]
        MissingStrategy,

        [Description("Select a video")]
        MissingVideo,

        [Description("Enter the username")]
        MissingUserName,

        [Description("Enter the token")]
        MissingToken,

        [Description("per view")]
        PerView,

        [Description("Add channel")]
        AddChannel,

        [Description("Edit channel")]
        EditChannel,

        [Description("Advertising strategy")]
        AdvertisingStrategy,

        [Description("Name")]
        Name,

        [Description("Low")]
        Low,

        [Description("Normal")]
        Normal,

        [Description("High")]
        High,

        [Description("Aggressive")]
        Aggressive,

        [Description("Shoot video")]
        ShootVideo,

        [Description("Edit video")]
        EditVideo,

        [Description("Study")]
        Study,

        [Description("Jumped the gun")]
        TooSoon,

        [Description("There are no videos to edit!")]
        NoVideosToEdit,

        [Description("points left")]
        PointsLeft,

        [Description("How you gonna pay for that?")]
        LowCashHeader,

        [Description("Not enough cash!")]
        LowCashMessage,

        [Description("Hours")]
        Hours,

        [Description("Hours left: {0}")]
        HoursLeft,

        [Description("Add video")]
        AddVideo,

        [Description("Attributes")]
        Attributes,

        [Description("Cost")]
        Cost,

        [Description("Category")]
        Category,

        [Description("Suspended")]
        Suspended,

        [Description("Video")]
        Video,

        [Description("Videos")]
        Videos,

        [Description("Add Task")]
        AddTask,

        [Description("Quit your job")]
        QuitJobHeader,

        [Description("Taking this action will quit your job. \nAre you sure?")]
        QuitJobText,

        [Description("Rise up!")]
        RiseUp,

        [Description("You need at least {0} to start a rebellion against our robot masters!")]
        RebellionCashRequired,

        [Description("Taking this action will start an uprising against our robot masters!\nYou have a {0}% chance of success and failure will result in DEATH! \n\nAre you sure?")]
        RebellionStart,

        [Description("Freedom!")]
        Freedom,

        [Description("You find the master switch and switch the master robot race off!\nThat was easy!")]
        RebellionSuccess,

        [Description("Remove task")]
        RemoveTask,

        [Description("Are you sure?")]
        AreYouSure,

        [Description("No refunds!")]
        NoRefunds,

        [Description("Casual")]
        Casual,

        [Description("Challenge")]
        Challenge,

        [Description("Failure")]
        Failure,

        [Description("The login failed... maybe try again?")]
        LoginFailure,

        [Description("User name")]
        Username,

        [Description("Token")]
        Token,

        [Description("Hours to complete")]
        HoursToComplete,

        [Description("Prerequisites not met!")]
        PrerequisitesNotMet,

        [Description("Already completed")]
        AlreadyCompleted,

        [Description("Already enrolled")]
        AlreadyEnrolled,

        [Description("Study selection")]
        StudySelection,

        [Description("Upload video")]
        UploadVideo,

        [Description("Random image")]
        FetchNewImage,

        [Description("Image")]
        Image,

        [Description("Buy views")]
        BuyViews,

        [Description("Channel")]
        Channel,

        [Description("Views")]
        Views,

        [Description("None")]
        None,

        [Description("Comments")]
        Comments,

        [Description("Quality")]
        Quality,

        [Description("N/A")]
        NotApplicable,

        [Description("Likes")]
        Likes,

        [Description("Dislikes")]
        Dislikes,

        [Description("Subscribers")]
        Subscribers,

        [Description("Channel Income")]
        ChannelIncome,

        [Description("Where to?")]
        WhereTo,

        [Description("Create a channel to upload to!")]
        ChannelNeeded,

        [Description("Video Manager")]
        VideoManager,

        [Description("Daily Planner")]
        DailyPlanner,

        [Description("Stats")]
        Stats,

        [Description("Subscriber History")]
        SubscriberHistory,

        [Description("Daily Income and Expenses")]
        DailyIncome,

        [Description("Generic Camcorder XYZ-85")]
        VideoCameraIStoreItem,

        [Description("It's better than a webcam...")]
        VideoCameraIStoreItemDescription,

        [Description("Super Camcorder Pro Deluxe HD")]
        VideoCameraIIStoreItem,

        [Description("Best video camera on the market! Used by all the pros!")]
        VideoCameraIIStoreItemDescription,

        [Description("Edit-Me Video Director 4")]
        EditingSoftwareIStoreItem,

        [Description("Better than the one that comes free with my OS...")]
        EditingSoftwareIStoreItemDescription,

        [Description("Mega-Director Ultra Deluxe Edition 12")]
        EditingSoftwareIIStoreItem,

        [Description("The ultimate in video editing software! 20 different star swipes to choose from!")]
        EditingSoftwareIIStoreItemDescription,

        [Description("Wright & Wrong Attorneys")]
        LawyerStoreItem,

        [Description("For all your legal needs! We'll unblock any suspended video or channel for a small fee of {0}!")]
        LawyerStoreItemDescription,

        [Description("Bob Boskins - Consultant")]
        ConsultantStoreItem,

        [Description("With my expert guidance, I'll make you a TubeStar! I also charge {0} per video!")]
        ConsultantStoreItemDescription,

        [Description("Buy")]
        Buy,

        [Description("Online Store")]
        OnlineStore,

        [Description("(Click to Hire Lawyer)")]
        SuspendedHireLawyer,

        [Description("Do you want to pay our fee of {0} to lift this video's suspension?")]
        RemoveVideoSuspension,

        [Description("Do you want to pay our fee of {0} to lift this channel's suspension?")]
        RemoveChannelSuspension,

        [Description("Shylock's Loans")]
        LoanStoreItem,

        [Description("Immediate payout of {0} guaranteed!! Call today! \nPay back {1} per day. {2}% interest.")]
        LoanStoreItemDescription,

        [Description("Edit")]
        Edit,

        [Description("Save")]
        Save,

        [Description("Add Translation")]
        AddTranslation,

        [Description("View Video")]
        ViewVideo,

        [Description("Born to Rule")]
        BornToRule,

        [Description("Buy for {0}?")]
        BuyItem,

        [Description("Next")]
        Next,

        [Description("The game was successfully saved!")]
        SaveGameSuccess,

        [Description("The robots take 20% of your money in tribute to their glory.")]
        RobotTax,

        [Description("Top TubeStars")]
        TopTubeStars,

        [Description("Ultra")]
        Ultra,

        [Description("Delete")]
        Delete,

        [Description("Delete Video")]
        DeleteVideo,

        [Description("Display 'Creative Commons' Material Only")]
        UseCreativeCommons,
    }
}