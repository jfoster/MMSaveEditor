﻿// Decompiled with JetBrains decompiler
// Type: UIEventCalendarQualifyingEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventCalendarQualifyingEntry : MonoBehaviour
{
  public UIEventCalendarQualifyingEntry.BarType barType;
  public GameObject[] bars;
  public Button button;
  public Image teamStrip;
  public Flag driverFlag;
  public TextMeshProUGUI position;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI laps;
  public TextMeshProUGUI bestLapTime;
  public TextMeshProUGUI qualifyinSession;
  private RaceEventResults.ResultData mResult;

  public void OnStart()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void Setup(RaceEventResults.ResultData inResult)
  {
    if (inResult == null)
      return;
    this.mResult = inResult;
    this.SetDetails();
  }

  private void SetDetails()
  {
    Team team = this.mResult.driver.contract.GetTeam();
    StringVariableParser.subject = (Person) this.mResult.driver;
    if (!this.mResult.driver.IsFreeAgent())
    {
      this.teamName.text = team.name;
      this.teamStrip.color = team.GetTeamColor().primaryUIColour.normal;
    }
    else
    {
      this.teamName.text = Localisation.LocaliseID("PSG_10011118", (GameObject) null);
      this.teamStrip.color = team.GetTeamColor().primaryUIColour.normal;
    }
    StringVariableParser.subject = (Person) null;
    this.driverFlag.SetNationality(this.mResult.driver.nationality);
    this.driverName.text = this.mResult.driver.shortName;
    this.position.text = this.mResult.position.ToString();
    this.laps.text = this.mResult.laps.ToString();
    if (this.mResult.qualifyingSession != 0)
      this.qualifyinSession.text = this.mResult.GetQualiSessionName();
    else
      this.qualifyinSession.text = string.Empty;
    if ((double) this.mResult.gapToLeader != 0.0)
      this.bestLapTime.text = GameUtility.GetGapTimeText(this.mResult.gapToLeader, false);
    else
      this.bestLapTime.text = "-";
    if (this.mResult.driver.IsPlayersDriver())
      this.barType = UIEventCalendarQualifyingEntry.BarType.PlayerOwned;
    this.SetBarType();
  }

  public void SetBarType()
  {
    for (int index = 0; index < this.bars.Length; ++index)
      GameUtility.SetActive(this.bars[index], (UIEventCalendarQualifyingEntry.BarType) index == this.barType);
  }

  private void OnButton()
  {
    if (this.mResult == null)
      return;
    UIManager.instance.ChangeScreen("DriverScreen", (Entity) this.mResult.driver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }
}