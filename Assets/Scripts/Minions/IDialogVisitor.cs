using System;
using Assets.Scripts.Ui;
using Ui;
using UnityEngine;
using UnityEngine.Events;

public interface IDialogVisitor
{
    // ReSharper disable once InconsistentNaming
    public void EnterToDialogue(Transform player,DialogSystem OnDialogEnd);
    public bool IsOpenToDialog();
}