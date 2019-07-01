using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    /// <summary>
    /// 現在アクティブなシーンのカメラを中継するSingleton
    /// </summary>
    class CameraManager
    {
        public Vector NowCamera
        {
            get => sceneManager.NowScene.Camera.GetPosition();
            set => sceneManager.NowScene.Camera.SetPositon(value);
        }

        private static CameraManager cameraManager = new CameraManager();
        public static CameraManager GetInstance() => cameraManager;
        SceneManager sceneManager;

        private CameraManager() => 
            sceneManager = SceneManager.GetInstance();
    }
}
