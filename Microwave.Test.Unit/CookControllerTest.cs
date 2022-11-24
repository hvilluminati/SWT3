﻿using System;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class CookControllerTest
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;
        private IButton addButton;
        private IButton subtractButton;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            powerTube = Substitute.For<IPowerTube>();
            addButton = Substitute.For<IButton>();
            subtractButton = Substitute.For<IButton>();

            uut = new CookController(timer, display, powerTube, addButton, subtractButton, ui);
        }

        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            uut.StartCooking(50, 60);

            timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(50, 60);

            powerTube.Received().TurnOn(50);
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 55);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            powerTube.Received().TurnOff();
        }

        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            ui.Received().CookingIsDone();
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            powerTube.Received().TurnOff();
        }


        [Test]
        public void Cooking_TimerAdded_DisplayCallsCorrectTime()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(60);
            addButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 5);
        }

        [Test]
        public void Cooking_TimerSubtracted_DisplayCallsCorrectTime()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(60);
            subtractButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(0, 55);
        }

        [TestCase(4)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-4)]
        public void Cooking_TimerSubtracted_DisplayShowsNoNegatives(int initialTime)
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(initialTime);
            subtractButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(0, 1);
        }

    }
}