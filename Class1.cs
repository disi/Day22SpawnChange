using UnityEngine;
using TheForest.Utils;
using TheForest.Utils.Settings;

namespace Day22SpawnChange
{
    public class Day22SpawnChange : MonoBehaviour
    {

        protected bool visible = false;
        protected GUIStyle labelStyle;

        [ModAPI.Attributes.ExecuteOnGameStart]
        static void AddMeToScene()
        {
            GameObject GO = new GameObject("__Day22SpawnChange__");
            GO.AddComponent<Day22SpawnChange>();
        }

        // original variables for spawn
        public static float skinnyMin = 0f;
        public static float skinnyMax = 3f;
        public static float skinnedMin = 1f;
        public static float skinnedMax = 4f;
        public static float regularMin = 1f;
        public static float regularMax = 2f;
        public static float paintedMin = 1f;
        public static float paintedMax = 4f;
        public static float skinnyPaleMin = 0f;
        public static float skinnyPaleMax = 0f;
        public static float paleMin = 0f;
        public static float paleMax = 0f;
        public static float creepyMin = 0f;
        public static float creepyMax = 3f;
        private bool updateButton;

        private void OnGUI()
        {
            if (this.visible)
            {
                GUI.skin = ModAPI.Gui.Skin;
                Matrix4x4 bkpMatrix = GUI.matrix;

                if (labelStyle == null)
                {
                    labelStyle = new GUIStyle(GUI.skin.label);
                    labelStyle.fontSize = 12;
                }

                GUI.Box(new Rect(10, 10, 400, 470), "Day22SpawnChange", GUI.skin.window);
                float cY = 50f;

                GUI.Label(new Rect(20f, cY, 150f, 20f), "SkinnyMin(0):" + (int)skinnyMin, labelStyle);
                skinnyMin = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), skinnyMin, 0, 10);
                cY += 25f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "SkinnyMax(3):" + (int)skinnyMax, labelStyle);
                skinnyMax = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), skinnyMax, 0, 10);
                cY += 30f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "SkinnedMin(1):" + (int)skinnedMin, labelStyle);
                skinnedMin = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), skinnedMin, 0, 10);
                cY += 25f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "SkinnedMax(4):" + (int)skinnedMax, labelStyle);
                skinnedMax = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), skinnedMax, 0, 10);
                cY += 30f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "RegularMin(1):" + (int)regularMin, labelStyle);
                regularMin = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), regularMin, 0, 10);
                cY += 25f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "RegularMax(2):" + (int)regularMax, labelStyle);
                regularMax = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), regularMax, 0, 10);
                cY += 30f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "PaintedMin(1):" + (int)paintedMin, labelStyle);
                paintedMin = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), paintedMin, 0, 10);
                cY += 25f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "PaintedMax(4):" + (int)paintedMax, labelStyle);
                paintedMax = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), paintedMax, 0, 10);
                cY += 30f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "SkinnyPaleMin(0):" + (int)skinnyPaleMin, labelStyle);
                skinnyPaleMin = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), skinnyPaleMin, 0, 10);
                cY += 25f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "SkinnyPaleMax(0):" + (int)skinnyPaleMax, labelStyle);
                skinnyPaleMax = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), skinnyPaleMax, 0, 10);
                cY += 30f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "PaleMin(0):" + (int)paleMin, labelStyle);
                paleMin = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), paleMin, 0, 10);
                cY += 25f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "PaleMax(0):" + (int)paleMax, labelStyle);
                paleMax = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), paleMax, 0, 10);
                cY += 30f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "CreepyMin(0): " + (int)creepyMin, labelStyle);
                creepyMin = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), creepyMin, 0, 10);
                cY += 25f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "CreepyMax(3): " + (int)creepyMax, labelStyle);
                creepyMax = GUI.HorizontalSlider(new Rect(170, cY + 3, 210, 30), creepyMax, 0, 10);
                cY += 30f;
                updateButton = GUI.Button(new Rect(20, cY + 3, 120, 30), "Update Spawns");
                cY += 30f;
                GUI.matrix = bkpMatrix;
                if (updateButton)
                {
                    UpdateSpawns();
                    TheForest.Utils.LocalPlayer.FpCharacter.UnLockView();
                    this.visible = !this.visible;
                }
            }
        }

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("OpenGUI"))
            {
                if (this.visible)
                {
                    TheForest.Utils.LocalPlayer.FpCharacter.UnLockView();
                }
                else
                {
                    TheForest.Utils.LocalPlayer.FpCharacter.LockView();
                }
                this.visible = !this.visible;
            }
        }

        private void UpdateSpawns()
        {
            Scene.MutantControler.disableWorldMutants();
            Scene.MutantControler.SendMessage("setDayConditions");
            Scene.MutantControler.activateNextSpawn();
        }
    }

    class D22SCSpawnManager : mutantSpawnManager
    {
        public override void setMutantSpawnAmounts()
        {
            // original code
            base.setMutantSpawnAmounts();
            // my changes
            UpdateSpawnAmounts();
        }

        private void UpdateSpawnAmounts()
        {
            base.desiredSkinned = Mathf.FloorToInt(Random.Range(Day22SpawnChange.skinnedMin, Day22SpawnChange.skinnedMax) * GameSettings.Ai.regularSpawnAmountRatio);
            base.desiredRegular = Mathf.FloorToInt(Random.Range(Day22SpawnChange.regularMin, Day22SpawnChange.regularMax) * GameSettings.Ai.regularSpawnAmountRatio);
            base.desiredPainted = Mathf.FloorToInt(Random.Range(Day22SpawnChange.paintedMin, Day22SpawnChange.paintedMax) * GameSettings.Ai.regularSpawnAmountRatio);
            base.desiredSkinnyPale = Mathf.FloorToInt(Random.Range(Day22SpawnChange.skinnyPaleMin, Day22SpawnChange.skinnyPaleMax) * GameSettings.Ai.regularSpawnAmountRatio);
            base.desiredPale = Mathf.FloorToInt(Random.Range(Day22SpawnChange.paleMin, Day22SpawnChange.paleMax) * GameSettings.Ai.regularSpawnAmountRatio);
            base.desiredCreepy = Mathf.FloorToInt(Random.Range(Day22SpawnChange.creepyMin, Day22SpawnChange.creepyMax) * GameSettings.Ai.creepySpawnAmountRatio);
            base.desiredSkinny = Mathf.FloorToInt(Random.Range(Day22SpawnChange.skinnyMin, Day22SpawnChange.skinnyMax) * GameSettings.Ai.skinnySpawnAmountRatio);

            base.maxSleepingSpawns = 0;
            base.setMaxAmounts();
        }
    }

    class D22SCMutantController : mutantController
    {
        protected override void setDayConditions()
        {
            // original code
            base.setDayConditions();
            // reset max mutants
            int maxmutants = Scene.MutantSpawnManager.maxCreepy + Scene.MutantSpawnManager.maxPainted + Scene.MutantSpawnManager.maxPale + Scene.MutantSpawnManager.maxRegular + Scene.MutantSpawnManager.maxSkinned + Scene.MutantSpawnManager.maxSkinny + Scene.MutantSpawnManager.maxSkinnyPale;
            //ModAPI.Log.Write("invoked SetDayConditions: " + maxmutants);
            base.maxActiveMutants = maxmutants;
            base.currentMaxActiveMutants = maxmutants;
        }
    }
}
