using System;
using System.Collections.Generic;using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;
using System.Diagnostics;

namespace DTXMania
{
    internal class CAct演奏Drums背景 : CActivity
    {
        // 本家っぽい背景を表示させるメソッド。
        //
        // 拡張性とかないんで。はい、ヨロシクゥ!
        //
        public CAct演奏Drums背景()
        {
            base.b活性化してない = true;
        }

        public void tFadeIn(int player)
        {
            this.ct上背景クリアインタイマー[player] = new CCounter( 0, 100, 2, CDTXMania.Timer );
            this.eFadeMode = EFIFOモード.フェードイン;
        }

        //public void tFadeOut(int player)
        //{
        //    this.ct上背景フェードタイマー[player] = new CCounter( 0, 100, 6, CDTXMania.Timer );
        //    this.eFadeMode = EFIFOモード.フェードアウト;
        //}

        public void ClearIn(int player)
        {
            this.ct上背景クリアインタイマー[player] = new CCounter(0, 100, 2, CDTXMania.Timer);
            this.ct上背景クリアインタイマー[player].n現在の値 = 0;
            this.ct上背景FIFOタイマー = new CCounter(0, 100, 2, CDTXMania.Timer);
            this.ct上背景FIFOタイマー.n現在の値 = 0;
        }

        public override void On活性化()
        {
            base.On活性化();
        }

        public override void On非活性化()
        {
            CDTXMania.t安全にDisposeする(ref this.ct上背景FIFOタイマー);
            for (int i = 0; i < 2; i++)
            {
                ct上背景スクロール用タイマー[i] = null;
                ct上背景2ndスクロール用タイマー[i] = null;
                ct上背景2nd下方向移動用タイマー[i] = null;
                ct上背景3rdスクロール用タイマー[i] = null;
                ct上背景3rd上下移動用タイマー[i] = null;
            }
            CDTXMania.t安全にDisposeする(ref this.ct下背景スクロール用タイマー1);
            CDTXMania.t安全にDisposeする(ref this.ct下背景2ndスクロール用タイマー);
            CDTXMania.t安全にDisposeする(ref this.ct下背景3rdスクロール用タイマー);
            CDTXMania.t安全にDisposeする(ref this.ct下背景3rd波モーション用タイマー);
            CDTXMania.t安全にDisposeする(ref this.ct亀スクロール用タイマー);
            CDTXMania.t安全にDisposeする(ref this.ct亀パターン用タイマー);
            //CDTXMania.t安全にDisposeする(ref this.ct桜モーション用タイマー);
            CDTXMania.t安全にDisposeする(ref this.ct桜X移動用タイマー1);
            CDTXMania.t安全にDisposeする(ref this.ct桜Y移動用タイマー1);
            //CDTXMania.t安全にDisposeする(ref this.ct桜モーション用タイマー1);
            CDTXMania.t安全にDisposeする(ref this.ct桜X移動用タイマー2);
            CDTXMania.t安全にDisposeする(ref this.ct桜Y移動用タイマー2);
            //CDTXMania.t安全にDisposeする(ref this.ct桜モーション用タイマー2);
            CDTXMania.t安全にDisposeする(ref this.ct桜X移動用タイマー3);
            CDTXMania.t安全にDisposeする(ref this.ct桜Y移動用タイマー3);
            //CDTXMania.t安全にDisposeする(ref this.ct桜モーション用タイマー3);
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            //this.tx上背景メイン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Upper_BG\01\bg.png" ) );
            //this.tx上背景クリアメイン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Upper_BG\01\bg_clear.png" ) );
            //this.tx下背景メイン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Dancer_BG\01\bg.png" ) );
            //this.tx下背景クリアメイン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Dancer_BG\01\bg_clear.png" ) );
            //this.tx下背景クリアサブ1 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Dancer_BG\01\bg_clear_01.png" ) );
            //this.ct上背景スクロール用タイマー = new CCounter( 1, 328, 40, CDTXMania.Timer );
            this.ct上背景スクロール用タイマー = new CCounter[2];
            this.ct上背景2ndスクロール用タイマー = new CCounter[2];
            this.ct上背景2nd下方向移動用タイマー = new CCounter[2];
            this.ct上背景3rdスクロール用タイマー = new CCounter[2];
            this.ct上背景3rd上下移動用タイマー = new CCounter[2];
            this.ct上背景クリアインタイマー = new CCounter[2];
            for (int i = 0; i < 2; i++)
            {
                if (CDTXMania.Tx.Background_Up[i] != null)
                {
                    this.ct上背景スクロール用タイマー[i] = new CCounter(1, CDTXMania.Tx.Background_Up[i].szテクスチャサイズ.Width, 13, CDTXMania.Timer);
                    this.ct上背景クリアインタイマー[i] = new CCounter();
                }
                if (CDTXMania.Tx.Background_Up_2nd[i] != null)
                {
                    this.ct上背景2ndスクロール用タイマー[i] = new CCounter(1, CDTXMania.Tx.Background_Up_2nd[i].szテクスチャサイズ.Width, 3, CDTXMania.Timer);
                    this.ct上背景2nd下方向移動用タイマー[i] = new CCounter(1, 300, 5, CDTXMania.Timer);
                    this.ct上背景クリアインタイマー[i] = new CCounter();
                }
                if (CDTXMania.Tx.Background_Up_3rd[i] != null)
                {
                    this.ct上背景3rdスクロール用タイマー[i] = new CCounter(1, CDTXMania.Tx.Background_Up_3rd[i].szテクスチャサイズ.Width, 13, CDTXMania.Timer);
                    this.ct上背景3rd上下移動用タイマー[i] = new CCounter(1, 50, 50, CDTXMania.Timer);
                    this.ct上背景クリアインタイマー[i] = new CCounter();
                }
            }
            if (CDTXMania.Tx.Background_Down_Scroll != null)
                this.ct下背景スクロール用タイマー1 = new CCounter( 1, CDTXMania.Tx.Background_Down_Scroll.szテクスチャサイズ.Width, 4, CDTXMania.Timer );

            if (CDTXMania.Tx.Background_Down_Clear_2nd != null)
                this.ct下背景2ndスクロール用タイマー = new CCounter(1, CDTXMania.Tx.Background_Down_Clear_2nd.szテクスチャサイズ.Height, 10, CDTXMania.Timer);

            if (CDTXMania.Tx.Background_Down_Clear_3rd != null)
                this.ct下背景3rdスクロール用タイマー = new CCounter(1, 750, 5, CDTXMania.Timer);
                this.ct下背景3rd波モーション用タイマー = new CCounter(1, 750, 5, CDTXMania.Timer);

            if (CDTXMania.Tx.Background_Down_kame != null)
                this.ct亀スクロール用タイマー = new CCounter(1, 1400, 4, CDTXMania.Timer);
                this.ct亀パターン用タイマー = new CCounter(0, 5, 170, CDTXMania.Timer);

            if (CDTXMania.Tx.Background_Down_sakura != null)
                //this.ct桜モーション用タイマー = new CCounter(0, 1000, 10, CDTXMania.Timer);
                this.ct桜X移動用タイマー1 = new CCounter(0, 166, 15, CDTXMania.Timer);
                this.ct桜Y移動用タイマー1 = new CCounter(0, 500, 5, CDTXMania.Timer);
                //this.ct桜モーション用タイマー1 = new CCounter(0, 10000, 1, CDTXMania.Timer);
                this.ct桜X移動用タイマー2 = new CCounter(0, 250, 10, CDTXMania.Timer);
                this.ct桜Y移動用タイマー2 = new CCounter(0, 500, 5, CDTXMania.Timer);
                //this.ct桜モーション用タイマー2 = new CCounter(0, 10000, 1, CDTXMania.Timer);
                this.ct桜X移動用タイマー3 = new CCounter(0, 333, 15, CDTXMania.Timer);
                this.ct桜Y移動用タイマー3 = new CCounter(0, 500, 10, CDTXMania.Timer);
                //this.ct桜モーション用タイマー3 = new CCounter(0, 10000, 1, CDTXMania.Timer);

            this.ct上背景FIFOタイマー = new CCounter();
            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            //CDTXMania.tテクスチャの解放( ref this.tx上背景メイン );
            //CDTXMania.tテクスチャの解放( ref this.tx上背景クリアメイン );
            //CDTXMania.tテクスチャの解放( ref this.tx下背景メイン );
            //CDTXMania.tテクスチャの解放( ref this.tx下背景クリアメイン );
            //CDTXMania.tテクスチャの解放( ref this.tx下背景クリアサブ1 );
            //Trace.TraceInformation("CActDrums背景 リソースの開放");
            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            this.ct上背景FIFOタイマー.t進行();
            
            for (int i = 0; i < 2; i++)
            {
                if(this.ct上背景クリアインタイマー[i] != null)
                   this.ct上背景クリアインタイマー[i].t進行();
            }
            for (int i = 0; i < 2; i++)
            {
                if (this.ct上背景スクロール用タイマー[i] != null)
                    this.ct上背景スクロール用タイマー[i].t進行Loop();
            }
            for (int i = 0; i < 2; i++)
            {
                if (this.ct上背景2ndスクロール用タイマー[i] != null)
                    this.ct上背景2ndスクロール用タイマー[i].t進行Loop();
            }
            for (int i = 0; i < 2; i++)
            {
                if (this.ct上背景2nd下方向移動用タイマー[i] != null)
                    this.ct上背景2nd下方向移動用タイマー[i].t進行Loop();
            }
            for (int i = 0; i < 2; i++)
            {
                if (this.ct上背景3rdスクロール用タイマー[i] != null)
                    this.ct上背景3rdスクロール用タイマー[i].t進行Loop();
            }
            for (int i = 0; i < 2; i++)
            {
                if (this.ct上背景3rd上下移動用タイマー[i] != null)
                    this.ct上背景3rd上下移動用タイマー[i].t進行Loop();
            }
            if (this.ct下背景スクロール用タイマー1 != null)
                this.ct下背景スクロール用タイマー1.t進行Loop();

            if (this.ct下背景2ndスクロール用タイマー != null)
                this.ct下背景2ndスクロール用タイマー.t進行Loop();

            if (this.ct下背景3rdスクロール用タイマー != null)
                this.ct下背景3rdスクロール用タイマー.t進行Loop();

            if (this.ct下背景3rd波モーション用タイマー != null)
                this.ct下背景3rd波モーション用タイマー.t進行Loop();

            if (this.ct亀スクロール用タイマー != null)
                this.ct亀スクロール用タイマー.t進行Loop();

            if (this.ct亀パターン用タイマー != null)
                this.ct亀パターン用タイマー.t進行Loop();

            //if (this.ct桜モーション用タイマー != null)
                //this.ct桜モーション用タイマー.t進行Loop();

            if (this.ct桜X移動用タイマー1 != null)
                this.ct桜X移動用タイマー1.t進行Loop();

            if (this.ct桜Y移動用タイマー1 != null)
                this.ct桜Y移動用タイマー1.t進行Loop();

            //if (this.ct桜モーション用タイマー1 != null)
                //this.ct桜モーション用タイマー1.t進行Loop();

            if (this.ct桜X移動用タイマー2 != null)
                this.ct桜X移動用タイマー2.t進行Loop();

            if (this.ct桜Y移動用タイマー2 != null)
                this.ct桜Y移動用タイマー2.t進行Loop();

            //if (this.ct桜モーション用タイマー2 != null)
                //this.ct桜モーション用タイマー2.t進行Loop();

            if (this.ct桜X移動用タイマー3 != null)
                this.ct桜X移動用タイマー3.t進行Loop();

            if (this.ct桜Y移動用タイマー3 != null)
                this.ct桜Y移動用タイマー3.t進行Loop();

            //if (this.ct桜モーション用タイマー3 != null)
                //this.ct桜モーション用タイマー3.t進行Loop();

            #region 1P-2P-上背景
            for (int i = 0; i < 2; i++)
            {
                if (this.ct上背景スクロール用タイマー[i] != null)
                {
                    double TexSize = 1280 / CDTXMania.Tx.Background_Up[i].szテクスチャサイズ.Width;
                    // 1280をテクスチャサイズで割ったものを切り上げて、プラス+1足す。
                    int ForLoop = (int)Math.Ceiling(TexSize) + 1;
                    //int nループ幅 = 328;
                    CDTXMania.Tx.Background_Up[i].t2D描画(CDTXMania.app.Device, 0 - this.ct上背景スクロール用タイマー[i].n現在の値, CDTXMania.Skin.Background_Scroll_Y[i]);
                    for (int l = 1; l < ForLoop + 1; l++)
                    {
                        CDTXMania.Tx.Background_Up[i].t2D描画(CDTXMania.app.Device, +(l * CDTXMania.Tx.Background_Up[i].szテクスチャサイズ.Width) - this.ct上背景スクロール用タイマー[i].n現在の値, CDTXMania.Skin.Background_Scroll_Y[i]);
                    }
                }
                if (this.ct上背景2ndスクロール用タイマー[i] != null && this.ct上背景2nd下方向移動用タイマー[i] != null)
                {
                    double TexSize = 1280 / CDTXMania.Tx.Background_Up_2nd[i].szテクスチャサイズ.Width;
                    // 1280をテクスチャサイズで割ったものを切り上げて、プラス+1足す。
                    int ForLoop = (int)Math.Ceiling(TexSize) + 1;
                    //int nループ幅 = 328;
                    CDTXMania.Tx.Background_Up_2nd[i].t2D描画(CDTXMania.app.Device, 0 - this.ct上背景2ndスクロール用タイマー[i].n現在の値, CDTXMania.Skin.Background_Scroll_Y[i] - 100 + this.ct上背景2nd下方向移動用タイマー[i].n現在の値);
                    for (int l = 1; l < ForLoop + 1; l++)
                    {
                        CDTXMania.Tx.Background_Up_2nd[i].t2D描画(CDTXMania.app.Device, +(l * CDTXMania.Tx.Background_Up_2nd[i].szテクスチャサイズ.Width) - this.ct上背景2ndスクロール用タイマー[i].n現在の値, CDTXMania.Skin.Background_Scroll_Y[i] - 100 + this.ct上背景2nd下方向移動用タイマー[i].n現在の値);
                    }
                }
                if (this.ct上背景3rdスクロール用タイマー[i] != null && this.ct上背景3rd上下移動用タイマー[i] != null)
                {

                    int testmotion;
                    
                    if (this.ct上背景3rd上下移動用タイマー[i].n現在の値 < 25)
                    {
                        testmotion = CDTXMania.Skin.Background_Scroll_Y[i] - 10 - this.ct上背景3rd上下移動用タイマー[i].n現在の値;
                    }
                    else
                    {
                        testmotion = CDTXMania.Skin.Background_Scroll_Y[i] - 60 + this.ct上背景3rd上下移動用タイマー[i].n現在の値;
                    }

                    double TexSize = 1280 / CDTXMania.Tx.Background_Up_3rd[i].szテクスチャサイズ.Width;
                    // 1280をテクスチャサイズで割ったものを切り上げて、プラス+1足す。
                    int ForLoop = (int)Math.Ceiling(TexSize) + 1;
                    //int nループ幅 = 328;

                    CDTXMania.Tx.Background_Up_3rd[i].t2D描画(CDTXMania.app.Device, 0 - this.ct上背景3rdスクロール用タイマー[i].n現在の値, testmotion);
                    for (int l = 1; l < ForLoop + 1; l++)
                    {
                        CDTXMania.Tx.Background_Up_3rd[i].t2D描画(CDTXMania.app.Device, +(l * CDTXMania.Tx.Background_Up_3rd[i].szテクスチャサイズ.Width) - this.ct上背景3rdスクロール用タイマー[i].n現在の値, testmotion);
                    }
                }
                if (this.ct上背景スクロール用タイマー[i] != null)
                {
                    if (CDTXMania.stage演奏ドラム画面.bIsAlreadyCleared[i])
                        CDTXMania.Tx.Background_Up_Clear[i].n透明度 = ((this.ct上背景クリアインタイマー[i].n現在の値 * 0xff) / 100);
                    else
                        CDTXMania.Tx.Background_Up_Clear[i].n透明度 = 0;

                    double TexSize = 1280 / CDTXMania.Tx.Background_Up_Clear[i].szテクスチャサイズ.Width;
                    // 1280をテクスチャサイズで割ったものを切り上げて、プラス+1足す。
                    int ForLoop = (int)Math.Ceiling(TexSize) + 1;

                    CDTXMania.Tx.Background_Up_Clear[i].t2D描画(CDTXMania.app.Device, 0 - this.ct上背景スクロール用タイマー[i].n現在の値, CDTXMania.Skin.Background_Scroll_Y[i]);
                    for (int l = 1; l < ForLoop + 1; l++)
                    {
                        CDTXMania.Tx.Background_Up_Clear[i].t2D描画(CDTXMania.app.Device, (l * CDTXMania.Tx.Background_Up_Clear[i].szテクスチャサイズ.Width) - this.ct上背景スクロール用タイマー[i].n現在の値, CDTXMania.Skin.Background_Scroll_Y[i]);
                    }
                }
                if (this.ct上背景2ndスクロール用タイマー[i] != null)
                {
                    if (CDTXMania.stage演奏ドラム画面.bIsAlreadyCleared[i])
                        CDTXMania.Tx.Background_Up_Clear_2nd[i].n透明度 = ((this.ct上背景クリアインタイマー[i].n現在の値 * 0xff) / 100);
                    else
                        CDTXMania.Tx.Background_Up_Clear_2nd[i].n透明度 = 0;

                    double TexSize = 1280 / CDTXMania.Tx.Background_Up_Clear_2nd[i].szテクスチャサイズ.Width;
                    // 1280をテクスチャサイズで割ったものを切り上げて、プラス+1足す。
                    int ForLoop = (int)Math.Ceiling(TexSize) + 1;

                    CDTXMania.Tx.Background_Up_Clear_2nd[i].t2D描画(CDTXMania.app.Device, 0 - this.ct上背景2ndスクロール用タイマー[i].n現在の値, CDTXMania.Skin.Background_Scroll_Y[i] - 100 + this.ct上背景2nd下方向移動用タイマー[i].n現在の値);
                    for (int l = 1; l < ForLoop + 1; l++)
                    {
                        CDTXMania.Tx.Background_Up_Clear_2nd[i].t2D描画(CDTXMania.app.Device, (l * CDTXMania.Tx.Background_Up_Clear_2nd[i].szテクスチャサイズ.Width) - this.ct上背景2ndスクロール用タイマー[i].n現在の値, CDTXMania.Skin.Background_Scroll_Y[i] - 100 + this.ct上背景2nd下方向移動用タイマー[i].n現在の値);
                    }
                }
                if (this.ct上背景3rdスクロール用タイマー[i] != null && this.ct上背景3rd上下移動用タイマー[i] != null)
                {

                    if (CDTXMania.stage演奏ドラム画面.bIsAlreadyCleared[i])
                        CDTXMania.Tx.Background_Up_Clear_3rd[i].n透明度 = ((this.ct上背景クリアインタイマー[i].n現在の値 * 0xff) / 100);
                    else
                        CDTXMania.Tx.Background_Up_Clear_3rd[i].n透明度 = 0;

                    int testmotion;

                    if (this.ct上背景3rd上下移動用タイマー[i].n現在の値 < 25)
                    {
                        testmotion = CDTXMania.Skin.Background_Scroll_Y[i] - 10 - this.ct上背景3rd上下移動用タイマー[i].n現在の値;
                    }
                    else
                    {
                        testmotion = CDTXMania.Skin.Background_Scroll_Y[i] - 60 + this.ct上背景3rd上下移動用タイマー[i].n現在の値;
                    }

                    double TexSize = 1280 / CDTXMania.Tx.Background_Up_Clear_3rd[i].szテクスチャサイズ.Width;
                    // 1280をテクスチャサイズで割ったものを切り上げて、プラス+1足す。
                    int ForLoop = (int)Math.Ceiling(TexSize) + 1;
                    //int nループ幅 = 328;

                    CDTXMania.Tx.Background_Up_Clear_3rd[i].t2D描画(CDTXMania.app.Device, 0 - this.ct上背景3rdスクロール用タイマー[i].n現在の値, testmotion);
                    for (int l = 1; l < ForLoop + 1; l++)
                    {
                        CDTXMania.Tx.Background_Up_Clear_3rd[i].t2D描画(CDTXMania.app.Device, +(l * CDTXMania.Tx.Background_Up_Clear_3rd[i].szテクスチャサイズ.Width) - this.ct上背景3rdスクロール用タイマー[i].n現在の値, testmotion);
                    }

                }

            }
            #endregion
            #region 1P-下背景
            if( !CDTXMania.stage演奏ドラム画面.bDoublePlay )
            {
                {
                    if( CDTXMania.Tx.Background_Down != null )
                    {
                        CDTXMania.Tx.Background_Down.t2D描画( CDTXMania.app.Device, 0, 360 );
                    }

                    if (CDTXMania.Tx.Background_Down_kame[this.ct亀パターン用タイマー.n現在の値] != null)
                    {
                        CDTXMania.Tx.Background_Down_kame[this.ct亀パターン用タイマー.n現在の値].t2D描画(CDTXMania.app.Device, 1300 - this.ct亀スクロール用タイマー.n現在の値, 550);
                    }

                    if (CDTXMania.Tx.Background_Down_2nd != null)
                    {
                        CDTXMania.Tx.Background_Down_2nd.t2D描画(CDTXMania.app.Device, 0, 360);
                    }

                    #region 桜モーション
                    if (CDTXMania.Tx.Background_Down_sakura != null)
                    {
                        #region 没モーション
                        /*
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 100 + this.ct桜モーション用タイマー.n現在の値 + ((float)Math.Sin((float)this.ct桜モーション用タイマー.n現在の値 * (Math.PI / 10)) * 100), 350 + this.ct桜モーション用タイマー.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 900 - this.ct桜モーション用タイマー.n現在の値 + ((float)Math.Sin((float)this.ct桜モーション用タイマー.n現在の値 * (Math.PI / 10)) * 100), 400 + this.ct桜Y移動用タイマー1.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 1300 - this.ct桜モーション用タイマー.n現在の値 + ((float)Math.Sin((float)this.ct桜モーション用タイマー.n現在の値 * (Math.PI / 10)) * 100), 300 + this.ct桜モーション用タイマー.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 0 + this.ct桜モーション用タイマー.n現在の値 + ((float)Math.Sin((float)this.ct桜モーション用タイマー.n現在の値 * (Math.PI / 10)) * 100), 200 + this.ct桜モーション用タイマー.n現在の値);
                        */
                        #endregion

                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 900 - this.ct桜X移動用タイマー1.n現在の値, 400 + this.ct桜Y移動用タイマー1.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 100 + this.ct桜X移動用タイマー1.n現在の値, 350 + this.ct桜Y移動用タイマー1.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 1300 - this.ct桜X移動用タイマー1.n現在の値, 350 + this.ct桜Y移動用タイマー1.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 900 - this.ct桜X移動用タイマー2.n現在の値, 320 + this.ct桜Y移動用タイマー2.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 800 - this.ct桜X移動用タイマー3.n現在の値, 450 + this.ct桜Y移動用タイマー3.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 10 + this.ct桜X移動用タイマー3.n現在の値, 430 + this.ct桜Y移動用タイマー3.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 800 - this.ct桜X移動用タイマー1.n現在の値, 200 + this.ct桜Y移動用タイマー1.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 0 + this.ct桜X移動用タイマー1.n現在の値, 220 + this.ct桜Y移動用タイマー1.n現在の値);
                        CDTXMania.Tx.Background_Down_sakura.t2D描画(CDTXMania.app.Device, 1100 - this.ct桜X移動用タイマー1.n現在の値, 250 + this.ct桜Y移動用タイマー1.n現在の値);
                    }
                    #endregion

                }
                if (CDTXMania.stage演奏ドラム画面.bIsAlreadyCleared[0])
                {
                    if (CDTXMania.Tx.Background_Down_Clear_2nd != null)
                    {
                        CDTXMania.Tx.Background_Down_Clear_2nd.n透明度 = ((this.ct上背景FIFOタイマー.n現在の値 * 0xff) / 100);

                        double TexSize = 1280 / CDTXMania.Tx.Background_Down_Clear_2nd.szテクスチャサイズ.Width;

                        int ForLoop = (int)Math.Ceiling(TexSize) + 1;

                        CDTXMania.Tx.Background_Down_Clear_2nd.t2D描画(CDTXMania.app.Device, 0 - this.ct下背景2ndスクロール用タイマー.n現在の値, 360);
                        for (int l = 1; l < ForLoop + 1; l++)
                        {
                            CDTXMania.Tx.Background_Down_Clear_2nd.t2D描画(CDTXMania.app.Device, +(l * CDTXMania.Tx.Background_Down_Clear_2nd.szテクスチャサイズ.Width) - this.ct下背景2ndスクロール用タイマー.n現在の値, 360);
                        }
                    }

                    if ( CDTXMania.Tx.Background_Down_Clear != null && CDTXMania.Tx.Background_Down_Scroll != null )
                    {
                        CDTXMania.Tx.Background_Down_Clear.n透明度 = ( ( this.ct上背景FIFOタイマー.n現在の値 * 0xff ) / 100 );
                        CDTXMania.Tx.Background_Down_Scroll.n透明度 = ( ( this.ct上背景FIFOタイマー.n現在の値 * 0xff ) / 100 );
                        CDTXMania.Tx.Background_Down_Clear.t2D描画( CDTXMania.app.Device, 0, 360 );

                        //int nループ幅 = 1257;
                        //CDTXMania.Tx.Background_Down_Scroll.t2D描画( CDTXMania.app.Device, 0 - this.ct下背景スクロール用タイマー1.n現在の値, 360 );
                        //CDTXMania.Tx.Background_Down_Scroll.t2D描画(CDTXMania.app.Device, (1 * nループ幅) - this.ct下背景スクロール用タイマー1.n現在の値, 360);
                        double TexSize = 1280 / CDTXMania.Tx.Background_Down_Scroll.szテクスチャサイズ.Width;
                        // 1280をテクスチャサイズで割ったものを切り上げて、プラス+1足す。
                        int ForLoop = (int)Math.Ceiling(TexSize) + 1;

                        //int nループ幅 = 328;
                        CDTXMania.Tx.Background_Down_Scroll.t2D描画(CDTXMania.app.Device, 0 - this.ct下背景スクロール用タイマー1.n現在の値, 360);
                        for (int l = 1; l < ForLoop + 1; l++)
                        {
                            CDTXMania.Tx.Background_Down_Scroll.t2D描画(CDTXMania.app.Device, +(l * CDTXMania.Tx.Background_Down_Scroll.szテクスチャサイズ.Width) - this.ct下背景スクロール用タイマー1.n現在の値, 360);
                        }

                    }
                    if (CDTXMania.Tx.Background_Down_Clear_3rd != null)
                    {
                        CDTXMania.Tx.Background_Down_Clear_3rd.n透明度 = ((this.ct上背景FIFOタイマー.n現在の値 * 0xff) / 100);

                        double TexSize = 1280 / CDTXMania.Tx.Background_Down_Clear_3rd.szテクスチャサイズ.Width;

                        int ForLoop = (int)Math.Ceiling(TexSize) + 1;

                        CDTXMania.Tx.Background_Down_Clear_3rd.t2D描画(CDTXMania.app.Device, 1500 - CDTXMania.Tx.Background_Down_Clear_3rd.szテクスチャサイズ.Width - this.ct下背景3rdスクロール用タイマー.n現在の値, 580 - ((float)Math.Sin((float)this.ct下背景3rd波モーション用タイマー.n現在の値 * (Math.PI / 750)) * 220));
                    }
                    if (CDTXMania.Tx.Background_Down_Clear_4th != null)
                    {
                        CDTXMania.Tx.Background_Down_Clear_4th.n透明度 = ((this.ct上背景FIFOタイマー.n現在の値 * 0xff) / 100);
                        CDTXMania.Tx.Background_Down_Clear_4th.t2D描画(CDTXMania.app.Device, 0, 361);
                    }
                }
            }
            #endregion
            return base.On進行描画();
        }

        #region[ private ]
        //-----------------
        private CCounter[] ct上背景スクロール用タイマー; //上背景のX方向スクロール用
        private CCounter[] ct上背景2ndスクロール用タイマー;
        private CCounter[] ct上背景2nd下方向移動用タイマー;
        private CCounter[] ct上背景3rdスクロール用タイマー;
        private CCounter[] ct上背景3rd上下移動用タイマー;
        private CCounter ct下背景スクロール用タイマー1; //下背景パーツ1のX方向スクロール用
        private CCounter ct下背景2ndスクロール用タイマー;
        private CCounter ct下背景3rdスクロール用タイマー;
        private CCounter ct下背景3rd波モーション用タイマー;
        private CCounter ct上背景FIFOタイマー;
        private CCounter ct亀スクロール用タイマー;
        private CCounter ct亀パターン用タイマー;
        private CCounter ct桜X移動用タイマー1;
        private CCounter ct桜Y移動用タイマー1;
        //private CCounter ct桜モーション用タイマー1;
        private CCounter ct桜X移動用タイマー2;
        private CCounter ct桜Y移動用タイマー2;
        //private CCounter ct桜モーション用タイマー2;
        private CCounter ct桜X移動用タイマー3;
        private CCounter ct桜Y移動用タイマー3;
        //private CCounter ct桜モーション用タイマー3;
        //private CCounter ct桜モーション用タイマー;
        private CCounter[] ct上背景クリアインタイマー;
        //private CTexture tx上背景メイン;
        //private CTexture tx上背景クリアメイン;
        //private CTexture tx下背景メイン;
        //private CTexture tx下背景クリアメイン;
        //private CTexture tx下背景クリアサブ1;
        private EFIFOモード eFadeMode;
        //-----------------
        #endregion
    }
}
　
