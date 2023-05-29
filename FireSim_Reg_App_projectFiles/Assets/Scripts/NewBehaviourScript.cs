using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private void Start()
    {
        // Create a list to hold instances of different classes
        //List<BaseClass> classList = new List<BaseClass>();

        //// Add instances of different classes to the list
        //classList.Add(new ClassA());
        //classList.Add(new ClassB());
        //classList.Add(new ClassC());


    }
}

// Base class or interface that all the classes inherit or implement
public abstract class BaseClass
{
    public int evacuateScoreVal;
    public int alarmScoreVal;
    public int cylinSelectScore;
    public int pullPinScoreVal;
    public int distanceScoreVal;
    public int timeScoreVal;
    public int sweepCntScoreVal;
    public int sweepAngScoreVal;
    public int MatRemainScoreVal;
    public int totalScoreVal;
}

// Example class A
public class ClassA : BaseClass
{
    public new int evacuateScoreVal = ReportGenerationScript.instance.IndustrialKitchen.evacuateScoreVal;
    public new int alarmScoreVal = ReportGenerationScript.instance.IndustrialKitchen.alarmScoreVal;
    public new int cylinSelectScore = ReportGenerationScript.instance.IndustrialKitchen.cylinSelectScore;
    public new int pullPinScoreVal = ReportGenerationScript.instance.IndustrialKitchen.pullPinScoreVal;
    public new int distanceScoreVal= ReportGenerationScript.instance.IndustrialKitchen.distanceScoreVal;
    public new int timeScoreVal= ReportGenerationScript.instance.IndustrialKitchen.timeScoreVal;
    public new int sweepCntScoreVal= ReportGenerationScript.instance.IndustrialKitchen.sweepCntScoreVal;
    public new int sweepAngScoreVal= ReportGenerationScript.instance.IndustrialKitchen.sweepAngScoreVal;
    public new int MatRemainScoreVal= ReportGenerationScript.instance.IndustrialKitchen.MatRemainScoreVal;
    public new int totalScoreVal = ReportGenerationScript.instance.IndustrialKitchen.totalScoreVal;
}

// Example class B
public class ClassB : BaseClass
{ 
    public new int evacuateScoreVal = ReportGenerationScript.instance.LabMetal.evacuateScoreVal;
    public new int alarmScoreVal = ReportGenerationScript.instance.LabMetal.alarmScoreVal;
    public new int cylinSelectScore = ReportGenerationScript.instance.LabMetal.cylinSelectScore;
    public new int pullPinScoreVal = ReportGenerationScript.instance.LabMetal.pullPinScoreVal;
    public new int distanceScoreVal = ReportGenerationScript.instance.LabMetal.distanceScoreVal;
    public new int timeScoreVal = ReportGenerationScript.instance.LabMetal.timeScoreVal;
    public new int sweepCntScoreVal = ReportGenerationScript.instance.LabMetal.sweepCntScoreVal;
    public new int sweepAngScoreVal = ReportGenerationScript.instance.LabMetal.sweepAngScoreVal;
    public new int MatRemainScoreVal = ReportGenerationScript.instance.LabMetal.MatRemainScoreVal;
    public new int totalScoreVal = ReportGenerationScript.instance.LabMetal.totalScoreVal;
}

// Example class C
public class ClassC : BaseClass
{
    public new int evacuateScoreVal = ReportGenerationScript.instance.OfficePaper.evacuateScoreVal;
    public new int alarmScoreVal = ReportGenerationScript.instance.OfficePaper.alarmScoreVal;
    public new int cylinSelectScore = ReportGenerationScript.instance.OfficePaper.cylinSelectScore;
    public new int pullPinScoreVal = ReportGenerationScript.instance.OfficePaper.pullPinScoreVal;
    public new int distanceScoreVal = ReportGenerationScript.instance.OfficePaper.distanceScoreVal;
    public new int timeScoreVal = ReportGenerationScript.instance.OfficePaper.timeScoreVal;
    public new int sweepCntScoreVal = ReportGenerationScript.instance.OfficePaper.sweepCntScoreVal;
    public new int sweepAngScoreVal = ReportGenerationScript.instance.OfficePaper.sweepAngScoreVal;
    public new int MatRemainScoreVal = ReportGenerationScript.instance.OfficePaper.MatRemainScoreVal;
    public new int totalScoreVal = ReportGenerationScript.instance.OfficePaper.totalScoreVal;
}

public class ClassD : BaseClass
{
    public new int evacuateScoreVal = ReportGenerationScript.instance.OfficeElectric.evacuateScoreVal;
    public new int alarmScoreVal = ReportGenerationScript.instance.OfficeElectric.alarmScoreVal;
    public new int cylinSelectScore = ReportGenerationScript.instance.OfficeElectric.cylinSelectScore;
    public new int pullPinScoreVal = ReportGenerationScript.instance.OfficeElectric.pullPinScoreVal;
    public new int distanceScoreVal = ReportGenerationScript.instance.OfficeElectric.distanceScoreVal;
    public new int timeScoreVal = ReportGenerationScript.instance.OfficeElectric.timeScoreVal;
    public new int sweepCntScoreVal = ReportGenerationScript.instance.OfficeElectric.sweepCntScoreVal;
    public new int sweepAngScoreVal = ReportGenerationScript.instance.OfficeElectric.sweepAngScoreVal;
    public new int MatRemainScoreVal = ReportGenerationScript.instance.OfficeElectric.MatRemainScoreVal;
    public new int totalScoreVal = ReportGenerationScript.instance.OfficeElectric.totalScoreVal;
}

public class ClassE : BaseClass
{
    public new int evacuateScoreVal = ReportGenerationScript.instance.OfficeSofa.evacuateScoreVal;
    public new int alarmScoreVal = ReportGenerationScript.instance.OfficeSofa.alarmScoreVal;
    public new int cylinSelectScore = ReportGenerationScript.instance.OfficeSofa.cylinSelectScore;
    public new int pullPinScoreVal = ReportGenerationScript.instance.OfficeSofa.pullPinScoreVal;
    public new int distanceScoreVal = ReportGenerationScript.instance.OfficeSofa.distanceScoreVal;
    public new int timeScoreVal = ReportGenerationScript.instance.OfficeSofa.timeScoreVal;
    public new int sweepCntScoreVal = ReportGenerationScript.instance.OfficeSofa.sweepCntScoreVal;
    public new int sweepAngScoreVal = ReportGenerationScript.instance.OfficeSofa.sweepAngScoreVal;
    public new int MatRemainScoreVal = ReportGenerationScript.instance.OfficeSofa.MatRemainScoreVal;
    public new int totalScoreVal = ReportGenerationScript.instance.OfficeSofa.totalScoreVal;
}
public class ClassF : BaseClass
{

    public new int evacuateScoreVal = ReportGenerationScript.instance.WarehouseElectric.evacuateScoreVal;
    public new int alarmScoreVal = ReportGenerationScript.instance.WarehouseElectric.alarmScoreVal;
    public new int cylinSelectScore = ReportGenerationScript.instance.WarehouseElectric.cylinSelectScore;
    public new int pullPinScoreVal = ReportGenerationScript.instance.WarehouseElectric.pullPinScoreVal;
    public new int distanceScoreVal = ReportGenerationScript.instance.WarehouseElectric.distanceScoreVal;
    public new int timeScoreVal = ReportGenerationScript.instance.WarehouseElectric.timeScoreVal;
    public new int sweepCntScoreVal = ReportGenerationScript.instance.WarehouseElectric.sweepCntScoreVal;
    public new int sweepAngScoreVal = ReportGenerationScript.instance.WarehouseElectric.sweepAngScoreVal;
    public new int MatRemainScoreVal = ReportGenerationScript.instance.WarehouseElectric.MatRemainScoreVal;
    public new int totalScoreVal = ReportGenerationScript.instance.WarehouseElectric.totalScoreVal;
}
public class ClassG : BaseClass
{
    public new int evacuateScoreVal = ReportGenerationScript.instance.WarehouseWood.evacuateScoreVal;
    public new int alarmScoreVal = ReportGenerationScript.instance.WarehouseWood.alarmScoreVal;
    public new int cylinSelectScore = ReportGenerationScript.instance.WarehouseWood.cylinSelectScore;
    public new int pullPinScoreVal = ReportGenerationScript.instance.WarehouseWood.pullPinScoreVal;
    public new int distanceScoreVal = ReportGenerationScript.instance.WarehouseWood.distanceScoreVal;
    public new int timeScoreVal = ReportGenerationScript.instance.WarehouseWood.timeScoreVal;
    public new int sweepCntScoreVal = ReportGenerationScript.instance.WarehouseWood.sweepCntScoreVal;
    public new int sweepAngScoreVal = ReportGenerationScript.instance.WarehouseWood.sweepAngScoreVal;
    public new int MatRemainScoreVal = ReportGenerationScript.instance.WarehouseWood.MatRemainScoreVal;
    public new int totalScoreVal = ReportGenerationScript.instance.WarehouseElectric.totalScoreVal;
}
public class ClassH : BaseClass
{

    public new int evacuateScoreVal = ReportGenerationScript.instance.WarehouseOil.evacuateScoreVal;
    public new int alarmScoreVal = ReportGenerationScript.instance.WarehouseOil.alarmScoreVal;
    public new int cylinSelectScore = ReportGenerationScript.instance.WarehouseOil.cylinSelectScore;
    public new int pullPinScoreVal = ReportGenerationScript.instance.WarehouseOil.pullPinScoreVal;
    public new int distanceScoreVal = ReportGenerationScript.instance.WarehouseOil.distanceScoreVal;
    public new int timeScoreVal = ReportGenerationScript.instance.WarehouseOil.timeScoreVal;
    public new int sweepCntScoreVal = ReportGenerationScript.instance.WarehouseOil.sweepCntScoreVal;
    public new int sweepAngScoreVal = ReportGenerationScript.instance.WarehouseOil.sweepAngScoreVal;
    public new int MatRemainScoreVal = ReportGenerationScript.instance.WarehouseOil.MatRemainScoreVal;
    public new int totalScoreVal = ReportGenerationScript.instance.WarehouseOil.totalScoreVal;
}