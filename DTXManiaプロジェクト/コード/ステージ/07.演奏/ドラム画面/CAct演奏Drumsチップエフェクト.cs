using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Drumsチップエフェクト : CActivity
	{
		// コンストラクタ

		public CAct演奏Drumsチップエフェクト()
		{
			//base.b活性化してない = true;
		}
		
		
		// メソッド
        public virtual void Start(int nPlayer, int Lane)
		{
            if(CDTXMania.Tx.Gauge_Soul_Explosion != null)
            {
                for (int i = 0; i < 128; i++)
                {
                    if(!st[i].b使用中)
                    {
                        st[i].b使用中 = true;
                        st[i].b使用中3 = true;
                        st[i].ct進行 = new CCounter(0, 10, 20, CDTXMania.Timer);
                        st[i].ct進行3 = new CCounter(0, 440, 18, CDTXMania.Timer);
                        st[i].nプレイヤー = nPlayer;
                        st[i].Lane = Lane;
                        break;
                    }
                }
            }
		}

		// CActivity 実装

		public override void On活性化()
		{
            for (int i = 0; i < 128; i++)
            {
                st[i] = new STチップエフェクト
                {
                    b使用中 = false,

                    ct進行 = new CCounter(),
                    ct進行3 = new CCounter()
                };
            }
            base.On活性化();
		}
		public override void On非活性化()
		{
            for (int i = 0; i < 128; i++)
            {
                st[i].ct進行 = null;
                st[i].b使用中 = false;
            }
			base.On非活性化();
		}
		public override int On進行描画()
		{
            for (int i = 0; i < 128; i++)
            {
                if (st[i].b使用中)
                {
                    st[i].ct進行.t進行();
                    if (st[i].ct進行.b終了値に達した)
                    {
                        st[i].ct進行.t停止();
                        st[i].b使用中 = false;
                    }
                    switch (st[i].nプレイヤー)
                    {
                        case 0:
                            if (CDTXMania.Tx.Gauge_Soul_Explosion[0] != null)
                                CDTXMania.Tx.Gauge_Soul_Explosion[0].t2D描画(CDTXMania.app.Device, 1140, 73, new Rectangle(st[i].ct進行.n現在の値 * 140, 0, 140, 180));
                            CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                            break;
                        case 1:
                            if (CDTXMania.Tx.Gauge_Soul_Explosion[1] != null)
                                CDTXMania.Tx.Gauge_Soul_Explosion[1].t2D描画(CDTXMania.app.Device, 1140, 468, new Rectangle(st[i].ct進行.n現在の値 * 140, 0, 140, 180));
                            CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 557, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                            break;
                    }

                }

                if (st[i].b使用中3)
                {
                    st[i].ct進行3.t進行();
                    if (st[i].ct進行3.b終了値に達した)
                    {
                        st[i].ct進行3.t停止();
                        st[i].b使用中3 = false;
                    }

                    if (this.st[i].ct進行3.n現在の値 < 1)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 10;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 2)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 20;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 3)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 40;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 4)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 60;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 5)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 90;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 6)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 120;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 7)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 140;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 8)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 160;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 9)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 190;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 10)
                    {
                        CDTXMania.Tx.Notes.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                        CDTXMania.Tx.Notes_Hit.color4 = CDTXMania.Skin.cEffect;
                        CDTXMania.Tx.Notes_Hit.n透明度 = 220;
                        CDTXMania.Tx.Notes_Hit.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 11)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 255;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 12)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 240;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 13)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 220;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 14)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 200;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 15)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 180;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 16)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 160;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 17)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 140;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 18)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 120;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 19)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 100;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 20)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 80;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 21)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 60;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 22)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 40;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 23)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 20;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                    else if (this.st[i].ct進行3.n現在の値 < 24)
                    {
                        CDTXMania.Tx.Notes_Hit2.n透明度 = 10;
                        CDTXMania.Tx.Notes_Hit2.t2D中心基準描画(CDTXMania.app.Device, 1224, 162, new Rectangle(st[i].Lane * 130, 0, 130, 130));
                    }
                }
            }
                return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
        //private CTexture[] txChara;

        [StructLayout(LayoutKind.Sequential)]
        private struct STチップエフェクト
        {
            public bool b使用中;
            public CCounter ct進行3;
            public CCounter ct進行;
            public int nプレイヤー;
            public int Lane;
            internal bool b使用中3;
        }
        private STチップエフェクト[] st = new STチップエフェクト[128];
        //private struct ST連打キャラ
        //{
        //    public int nColor;
        //    public bool b使用中;
        //    public CCounter ct進行;
        //    public int n前回のValue;
        //    public float fX;
        //    public float fY;
        //    public float fX開始点;
        //    public float fY開始点;
        //    public float f進行方向; //進行方向 0:左→右 1:左下→右上 2:右→左
        //    public float fX加速度;
        //    public float fY加速度;
        //}
        //private ST連打キャラ[] st連打キャラ = new ST連打キャラ[64];
        //-----------------
        #endregion
    }
}
