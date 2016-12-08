using UnityEngine;
using System;
using UnityEngine.UI;

public class QuestTweener
{
    Quest TweenedQuest;
    QuestTextTweener _parent;
    GameObject player;
    Text QuestText;
    Vector3 QuestTextDefaultScale;
    Vector3 QuestTextTargetScaleIncrease = new Vector3(0.08f, 0.08f, 0);
    Vector3 QuestTextScaleIncreaseSpeed = new Vector3(0.2f, 0.2f, 0);
    Vector3 QuestTextAbsolutePosition;
    Vector3 questTargetPositionOffset = new Vector3(6, 0, 0);
    float questSpeed = 3.0f;
    float questThreshold = 0.5f;
    public QuestTweener(QuestTextTweener Parent, GameObject Player, Text QuestText)
    {
        _parent = Parent;
        player = Player;
        this.QuestText = QuestText;

        TweenedQuest = null;

        QuestTextAbsolutePosition = QuestText.transform.position - player.transform.position;
        QuestTextDefaultScale = QuestText.transform.localScale;
    }
    public bool StartTweening(Quest _quest)
    {
        if (_quest != null && TweenedQuest == null)
        {
            TweenedQuest = _quest;
            Debug.Log("Quest Tweening Started.");

            if (!TweenedQuest.isCompleted() || QuestText.text == "")
            {
                _parent.Tween += QuestSetTitleText;
                _parent.Tween += QuestFadeIn;
            }
            if (TweenedQuest.isCompleted())
            {
                _parent.Tween += QuestSetCompletedTitleText;
            }
            _parent.Tween += QuestScaleUp;

            return true;
        }
        return false;
    }
    void QuestScaleUp(object sender, EventArgs e)
    {
        QuestText.transform.localScale += QuestTextScaleIncreaseSpeed * Time.deltaTime;
        if (QuestText.transform.localScale.magnitude >= QuestTextDefaultScale.magnitude + QuestTextTargetScaleIncrease.magnitude)
        {
            QuestText.transform.localScale = QuestTextDefaultScale + QuestTextTargetScaleIncrease;
            _parent.Tween -= QuestScaleUp;
            _parent.Tween += QuestScaleDown;
        }
    }
    void QuestScaleDown(object sender, EventArgs e)
    {
        QuestText.transform.localScale -= QuestTextScaleIncreaseSpeed * Time.deltaTime;
        if (QuestText.transform.localScale.magnitude <= QuestTextDefaultScale.magnitude)
        {
            QuestText.transform.localScale = QuestTextDefaultScale;
            _parent.Tween -= QuestScaleDown;
            if (TweenedQuest.isCompleted())
            {
                _parent.Tween += QuestFadeOut;
                _parent.Tween += QuestMoveRight;
            }
            else
            {
                TweenedQuest = null;
            }
        }
    }
    void QuestFadeOut(object sender, EventArgs e)
    {
        _parent.Tween -= QuestFadeOut;
        QuestText.CrossFadeAlpha(0.00f, 1.00f, true);
    }
    void QuestFadeIn(object sender, EventArgs e)
    {
        _parent.Tween -= QuestFadeIn;
        QuestText.CrossFadeAlpha(0.00f, 0.00f, true);
        QuestText.CrossFadeAlpha(1.00f, 1.00f, true);
    }
    void QuestMoveRight(object sender, EventArgs e)
    {
        Vector3 questTargetPosition = player.transform.position + QuestTextAbsolutePosition + questTargetPositionOffset;
        Vector3 direction = questTargetPosition - QuestText.transform.position;
        if (direction.magnitude > questThreshold)
        {
            direction.Normalize();
            QuestText.transform.position = QuestText.transform.position + direction * questSpeed * Time.deltaTime;
        }
        else
        {
            // Without this game object jumps around target and never settles
            QuestText.transform.position = questTargetPosition;
            _parent.Tween -= QuestMoveRight;
            ResetQuestText();
            TweenedQuest = null;
        }
    }
    void QuestSetTitleText(object sender, EventArgs e)
    {
        _parent.Tween -= QuestSetTitleText;
        QuestText.text = TweenedQuest.GetTitle();
    }
    void QuestSetCompletedTitleText(object sender, EventArgs e)
    {
        _parent.Tween -= QuestSetCompletedTitleText;
        QuestText.text = TweenedQuest.GetTitle() + " (Completed)";
    }
    void ResetQuestText()
    {
        QuestText.text = "";
        QuestText.transform.position = player.transform.position + QuestTextAbsolutePosition;
        QuestText.CrossFadeAlpha(1.00f, 0.00f, true);
    }
}
