﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SBD.Domain.Models;
using SBD.InterfacePool;

namespace SBD.Old
{
    /// <summary>
    /// Page2BControl.xaml 的互動邏輯
    /// </summary>
    public partial class Page1BControl : UserControl, PageInterface, InterfacePool.IDataLinkInterface
    {
        WorkStepInterface _WorkStepInterface;
        public WorkStepInterface MainWorkStep { get => _WorkStepInterface; set => _WorkStepInterface = value; }

        public Page1BControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
       
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void PrintBT_TouchDown(object sender, TouchEventArgs e)
        {

        }

        private void Button_TouchUp(object sender, TouchEventArgs e)
        {

        }

        public void showWork()
        {
            throw new NotImplementedException();
        }

        public void CloseWork()
        {
            throw new NotImplementedException();
        }

        public void NGWork()
        {
            throw new NotImplementedException();
        }

        public void NextWork()
        {
            throw new NotImplementedException();
        }

        public void UPWork()
        {
            throw new NotImplementedException();
        }

        public void InputDataUser(BoardingPassData UserBoardingPassData)
        {
            throw new NotImplementedException();
        }

        public BoardingPassData OutputDataUser()
        {
            throw new NotImplementedException();
        }

        private void OKUSERDATA_TouchDown(object sender, TouchEventArgs e)
        {
            this.MainWorkStep.Step2();
        }
    }
}