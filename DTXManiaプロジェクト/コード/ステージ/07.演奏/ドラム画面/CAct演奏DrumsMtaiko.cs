using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using FDK;

namespace DTXMania
{
    internal class CAct演奏DrumsMtaiko : CActivity
    {
        /// <summary>
        /// mtaiko部分を描画するクラス。左側だけ。
        /// 
        /// </summary>
        public CAct演奏DrumsMtaiko()
        {
            //this.strCourseSymbolFileName = new string[]{
            //    @"Graphics\CourseSymbol\easy.png",
            //    @"Graphics\CourseSymbol\normal.png",
            //    @"Graphics\CourseSymbol\hard.png",
            //    @"Graphics\CourseSymbol\oni.png",
            //    @"Graphics\CourseSymbol\edit.png",
            //    @"Graphics\CourseSymbol\sinuchi.png",
            //};
            base.b活性化してない = true;
        }

        public override void On活性化()
        {
			for( int i = 0; i < 16; i++ )
			{
				STパッド状態 stパッド状態 = new STパッド状態();
				stパッド状態.n明るさ = 0;
				this.stパッド状態[ i ] = stパッド状態;
			}
            base.On活性化();
        }

        public override void On非活性化()
        {
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            //this.txMtaiko枠 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_mtaiko_A.png" ) );
            //this.txMtaiko下敷き[ 0 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_mtaiko_C.png" ) );
            //if (CDTXMania.stage演奏ドラム画面.bDoublePlay)
            //    this.txMtaiko下敷き[ 1 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_mtaiko_C_2P.png" ) );

            //this.txオプションパネル_HS = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_HiSpeed.png" ) );
            //this.txオプションパネル_RANMIR = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_RANMIR.png" ) );
            //this.txオプションパネル_特殊 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_SpecialOption.png" ) );
            
            //this.tx太鼓_土台 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_mtaiko_main.png" ) );
            //this.tx太鼓_面L = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_mtaiko_red.png" ) );
            //this.tx太鼓_ふちL = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_mtaiko_blue.png" ) );
            //this.tx太鼓_面R = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_mtaiko_red.png" ) );
            //this.tx太鼓_ふちR = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_mtaiko_blue.png" ) );

            //this.txレベルアップ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_LevelUp.png" ) );
            //this.txレベルダウン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_LevelDown.png" ) );

            //this.txネームプレート = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_NamePlate.png" ) );
            //if (CDTXMania.stage演奏ドラム画面.bDoublePlay)
            //    this.txネームプレート = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_NamePlate2P.png" ) );
            
            //for( int i = 0; i < 6; i++ )
            //{
            //    this.txコースシンボル[ i ] = CDTXMania.tテクスチャの生成( CSkin.Path( this.strCourseSymbolFileName[ i ] ) );
            //}
            this.ctレベルアップダウン = new CCounter[ 4 ];
            this.After = new int[ 4 ];
            this.Before = new int[ 4 ];
            for( int i = 0; i < 4; i++ )
            {
                //this.ctレベルアップダウン = new CCounter( 0, 1000, 1, CDTXMania.Timer );
                this.ctレベルアップダウン[ i ] = new CCounter();
            }

            int フェード間隔 = 3;

            if (CDTXMania.Tx.Taiko_PlateEffect_1 != null)
                this.ctエフェクト = new CCounter(0, 600, フェード間隔, CDTXMania.Timer);
                this.ctエフェクトフェード0 = new CCounter(0, 70, フェード間隔, CDTXMania.Timer);
                this.ctエフェクトフェード1 = new CCounter(0, 70, フェード間隔, CDTXMania.Timer);
                this.ctエフェクトフェード2 = new CCounter(0, 70, フェード間隔, CDTXMania.Timer);
                this.ctエフェクトフェード3 = new CCounter(0, 70, フェード間隔, CDTXMania.Timer);
                this.ctエフェクトフェード4 = new CCounter(0, 70, フェード間隔, CDTXMania.Timer);
                this.ctエフェクトフェード5 = new CCounter(0, 70, フェード間隔, CDTXMania.Timer);
                this.ctエフェクトフェード6 = new CCounter(0, 70, フェード間隔, CDTXMania.Timer);
                this.ctエフェクトフェード7 = new CCounter(0, 70, フェード間隔, CDTXMania.Timer);
                this.ctエフェクトスライド = new CCounter(0, 180, フェード間隔, CDTXMania.Timer);


            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
      //      CDTXMania.tテクスチャの解放( ref this.txMtaiko枠 );
      //      CDTXMania.tテクスチャの解放( ref this.txMtaiko下敷き[ 0 ] );
      //      if (CDTXMania.stage演奏ドラム画面.bDoublePlay)
      //          CDTXMania.tテクスチャの解放( ref this.txMtaiko下敷き[ 1 ] );
            
		    //CDTXMania.tテクスチャの解放( ref this.tx太鼓_土台 );
      //      CDTXMania.tテクスチャの解放( ref this.txオプションパネル_HS );
      //      CDTXMania.tテクスチャの解放( ref this.txオプションパネル_RANMIR );
      //      CDTXMania.tテクスチャの解放( ref this.txオプションパネル_特殊 );

      //      CDTXMania.tテクスチャの解放( ref this.tx太鼓_面L );
      //      CDTXMania.tテクスチャの解放( ref this.tx太鼓_面R );
		    //CDTXMania.tテクスチャの解放( ref this.tx太鼓_ふちL );
      //      CDTXMania.tテクスチャの解放( ref this.tx太鼓_ふちR );

		    //CDTXMania.tテクスチャの解放( ref this.txレベルアップ );
      //      CDTXMania.tテクスチャの解放( ref this.txレベルダウン );

      //      CDTXMania.tテクスチャの解放( ref this.txネームプレート );
      //      if (CDTXMania.stage演奏ドラム画面.bDoublePlay)
      //          CDTXMania.tテクスチャの解放( ref this.txネームプレート2P );

      //      for( int i = 0; i < 6; i++ )
      //      {
      //          CDTXMania.tテクスチャの解放( ref this.txコースシンボル[ i ] );
      //      }

            this.ctレベルアップダウン = null;

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            if( base.b初めての進行描画 )
			{
				this.nフラッシュ制御タイマ = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
				base.b初めての進行描画 = false;
            }

            this.ctエフェクト.t進行Loop();
            //this.ctエフェクトフェード.t進行Loop();
            //this.ctエフェクトスライド.t進行Loop();

            long num = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
			if( num < this.nフラッシュ制御タイマ )
			{
				this.nフラッシュ制御タイマ = num;
			}
			while( ( num - this.nフラッシュ制御タイマ ) >= 20 )
			{
				for( int j = 0; j < 16; j++ )
				{
					if( this.stパッド状態[ j ].n明るさ > 0 )
					{
						this.stパッド状態[ j ].n明るさ--;
					}
				}
				this.nフラッシュ制御タイマ += 20;
		    }


            this.nHS = CDTXMania.ConfigIni.n譜面スクロール速度.Drums < 8 ? CDTXMania.ConfigIni.n譜面スクロール速度.Drums : 7;

            //if(CDTXMania.Tx.Taiko_Frame[ 0 ] != null )
               // CDTXMania.Tx.Taiko_Frame[ 0 ].t2D描画( CDTXMania.app.Device, 0, 184 );
            if(CDTXMania.Tx.Taiko_Background[0] != null )
                CDTXMania.Tx.Taiko_Background[0].t2D描画( CDTXMania.app.Device, 0, 184 );

            if ( CDTXMania.stage演奏ドラム画面.bDoublePlay )
            {
                //if(CDTXMania.Tx.Taiko_Frame[ 1 ] != null )
                    //CDTXMania.Tx.Taiko_Frame[ 1 ].t2D描画( CDTXMania.app.Device, 0, 360 );
                if(CDTXMania.Tx.Taiko_Background[1] != null )
                    CDTXMania.Tx.Taiko_Background[1].t2D描画( CDTXMania.app.Device, 0, 360 );
            }
            
            if(CDTXMania.Tx.Taiko_Base != null )
            {
                CDTXMania.Tx.Taiko_Base.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[0], CDTXMania.Skin.Game_Taiko_Y[0]);
                if( CDTXMania.stage演奏ドラム画面.bDoublePlay )
                    CDTXMania.Tx.Taiko_Base.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[1], CDTXMania.Skin.Game_Taiko_Y[1]);
            }
            if( CDTXMania.Tx.Taiko_Don_Left != null && CDTXMania.Tx.Taiko_Don_Right != null && CDTXMania.Tx.Taiko_Ka_Left != null && CDTXMania.Tx.Taiko_Ka_Right != null )
            {
                CDTXMania.Tx.Taiko_Ka_Left.n透明度 = this.stパッド状態[0].n明るさ * 73;
                CDTXMania.Tx.Taiko_Ka_Right.n透明度 = this.stパッド状態[1].n明るさ * 73;
                CDTXMania.Tx.Taiko_Don_Left.n透明度 = this.stパッド状態[2].n明るさ * 73;
                CDTXMania.Tx.Taiko_Don_Right.n透明度 = this.stパッド状態[3].n明るさ * 73;
            
                CDTXMania.Tx.Taiko_Ka_Left.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[0], CDTXMania.Skin.Game_Taiko_Y[0], new Rectangle( 0, 0, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Height) );
                CDTXMania.Tx.Taiko_Ka_Right.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[0] + CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Skin.Game_Taiko_Y[0], new Rectangle(CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, 0, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Height) );
                CDTXMania.Tx.Taiko_Don_Left.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[0], CDTXMania.Skin.Game_Taiko_Y[0], new Rectangle( 0, 0, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Height) );
                CDTXMania.Tx.Taiko_Don_Right.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[0] + CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Skin.Game_Taiko_Y[0], new Rectangle(CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, 0, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Height));
            }

            if( CDTXMania.Tx.Taiko_Don_Left != null && CDTXMania.Tx.Taiko_Don_Right != null && CDTXMania.Tx.Taiko_Ka_Left != null && CDTXMania.Tx.Taiko_Ka_Right != null )
            {
                CDTXMania.Tx.Taiko_Ka_Left.n透明度 = this.stパッド状態[4].n明るさ * 73;
                CDTXMania.Tx.Taiko_Ka_Right.n透明度 = this.stパッド状態[5].n明るさ * 73;
                CDTXMania.Tx.Taiko_Don_Left.n透明度 = this.stパッド状態[6].n明るさ * 73;
                CDTXMania.Tx.Taiko_Don_Right.n透明度 = this.stパッド状態[7].n明るさ * 73;
            
                CDTXMania.Tx.Taiko_Ka_Left.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[1], CDTXMania.Skin.Game_Taiko_Y[1], new Rectangle( 0, 0, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Height) );
                CDTXMania.Tx.Taiko_Ka_Right.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[1] + CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Skin.Game_Taiko_Y[1], new Rectangle(CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, 0, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Height) );
                CDTXMania.Tx.Taiko_Don_Left.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[1], CDTXMania.Skin.Game_Taiko_Y[1], new Rectangle( 0, 0, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Height) );
                CDTXMania.Tx.Taiko_Don_Right.t2D描画( CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_X[1] + CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Skin.Game_Taiko_Y[1], new Rectangle(CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, 0, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Width / 2, CDTXMania.Tx.Taiko_Ka_Right.szテクスチャサイズ.Height) );
            }

            int[] nLVUPY = new int[] { 127, 127, 0, 0 };

            for ( int i = 0; i < CDTXMania.ConfigIni.nPlayerCount; i++ )
            {
                if( !this.ctレベルアップダウン[ i ].b停止中 )
                {
                    this.ctレベルアップダウン[ i ].t進行();
                    if( this.ctレベルアップダウン[ i ].b終了値に達した ) {
                        this.ctレベルアップダウン[ i ].t停止();
                    }
                }
                if( ( this.ctレベルアップダウン[ i ].b進行中 && ( CDTXMania.Tx.Taiko_LevelUp != null && CDTXMania.Tx.Taiko_LevelDown != null ) ) && !CDTXMania.ConfigIni.bNoInfo )
                {
                    //this.ctレベルアップダウン[ i ].n現在の値 = 110;

                    //2017.08.21 kairera0467 t3D描画に変更。
                    float fScale = 1.0f;
                    int nAlpha = 255;
                    float[] fY = new float[] { 206, -206, 0, 0 };
                    if( this.ctレベルアップダウン[ i ].n現在の値 >= 0 && this.ctレベルアップダウン[ i ].n現在の値 <= 20 )
                    {
                        nAlpha = 60;
                        fScale = 1.14f;
                    }
                    else if( this.ctレベルアップダウン[ i ].n現在の値 >= 21 && this.ctレベルアップダウン[ i ].n現在の値 <= 40 )
                    {
                        nAlpha = 60;
                        fScale = 1.19f;
                    }
                    else if( this.ctレベルアップダウン[ i ].n現在の値 >= 41 && this.ctレベルアップダウン[ i ].n現在の値 <= 60 )
                    {
                        nAlpha = 220;
                        fScale = 1.23f;
                    }
                    else if( this.ctレベルアップダウン[ i ].n現在の値 >= 61 && this.ctレベルアップダウン[ i ].n現在の値 <= 80 )
                    {
                        nAlpha = 230;
                        fScale = 1.19f;
                    }
                    else if( this.ctレベルアップダウン[ i ].n現在の値 >= 81 && this.ctレベルアップダウン[ i ].n現在の値 <= 100 )
                    {
                        nAlpha = 240;
                        fScale = 1.14f;
                    }
                    else if( this.ctレベルアップダウン[ i ].n現在の値 >= 101 && this.ctレベルアップダウン[ i ].n現在の値 <= 120 )
                    {
                        nAlpha = 255;
                        fScale = 1.04f;
                    }
                    else
                    {
                        nAlpha = 255;
                        fScale = 1.0f;
                    }

                    SlimDX.Matrix mat = SlimDX.Matrix.Identity;
                    mat *= SlimDX.Matrix.Scaling( fScale, fScale, 1.0f );
                    mat *= SlimDX.Matrix.Translation( -329, fY[ i ], 0 );
                    if( this.After[ i ] - this.Before[ i ] >= 0 )
                    {
                        //レベルアップ
                        CDTXMania.Tx.Taiko_LevelUp.n透明度 = nAlpha;
                        CDTXMania.Tx.Taiko_LevelUp.t3D描画( CDTXMania.app.Device, mat );
                    }
                    else
                    {
                        CDTXMania.Tx.Taiko_LevelDown.n透明度 = nAlpha;
                        CDTXMania.Tx.Taiko_LevelDown.t3D描画( CDTXMania.app.Device, mat );
                    }
                }
            }

            for( int i = 0; i < CDTXMania.ConfigIni.nPlayerCount; i++ )
            {
                // 2018/7/1 一時的にオプション画像の廃止。オプション画像については後日作り直します。(AioiLight)
                //if( !CDTXMania.ConfigIni.bNoInfo && CDTXMania.Skin.eDiffDispMode != E難易度表示タイプ.mtaikoに画像で表示 )
                //{
                //    this.txオプションパネル_HS.t2D描画( CDTXMania.app.Device, 0, 230, new Rectangle( 0, this.nHS * 44, 162, 44 ) );
                //    switch( CDTXMania.ConfigIni.eRandom.Taiko )
                //    {
                //        case Eランダムモード.RANDOM:
                //            if( this.txオプションパネル_RANMIR != null )
                //                this.txオプションパネル_RANMIR.t2D描画( CDTXMania.app.Device, 0, 264, new Rectangle( 0, 0, 162, 44 ) );
                //            break;
                //        case Eランダムモード.HYPERRANDOM:
                //            if( this.txオプションパネル_RANMIR != null )
                //                this.txオプションパネル_RANMIR.t2D描画( CDTXMania.app.Device, 0, 264, new Rectangle( 0, 88, 162, 44 ) );
                //            break;
                //        case Eランダムモード.SUPERRANDOM:
                //            if( this.txオプションパネル_RANMIR != null )
                //                this.txオプションパネル_RANMIR.t2D描画( CDTXMania.app.Device, 0, 264, new Rectangle( 0, 132, 162, 44 ) );
                //            break;
                //        case Eランダムモード.MIRROR:
                //            if( this.txオプションパネル_RANMIR != null )
                //                this.txオプションパネル_RANMIR.t2D描画( CDTXMania.app.Device, 0, 264, new Rectangle( 0, 44, 162, 44 ) );
                //            break;
                //    }

                //    if( CDTXMania.ConfigIni.eSTEALTH == Eステルスモード.STEALTH )
                //        this.txオプションパネル_特殊.t2D描画( CDTXMania.app.Device, 0, 300, new Rectangle( 0, 0, 162, 44 ) );
                //    else if( CDTXMania.ConfigIni.eSTEALTH == Eステルスモード.DORON )
                //        this.txオプションパネル_特殊.t2D描画( CDTXMania.app.Device, 0, 300, new Rectangle( 0, 44, 162, 44 ) );
                //}
                if (CDTXMania.Tx.Couse_Symbol[CDTXMania.stage選曲.n確定された曲の難易度] != null)
                {
                    CDTXMania.Tx.Couse_Symbol[CDTXMania.stage選曲.n確定された曲の難易度].t2D描画(CDTXMania.app.Device,
                        CDTXMania.Skin.Game_CourseSymbol_X[i],
                        CDTXMania.Skin.Game_CourseSymbol_Y[i]
                        );
                }

                if (CDTXMania.ConfigIni.ShinuchiMode)
                {
                    if (CDTXMania.Tx.Couse_Symbol[5] != null)
                    {
                        CDTXMania.Tx.Couse_Symbol[5].t2D描画(CDTXMania.app.Device,
                            CDTXMania.Skin.Game_CourseSymbol_X[i],
                            CDTXMania.Skin.Game_CourseSymbol_Y[i]
                            );
                    }

                }


            }
            if (CDTXMania.Tx.Taiko_NamePlate[0] != null)
            {
                CDTXMania.Tx.Taiko_NamePlate[0].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0], CDTXMania.Skin.Game_Taiko_NamePlate_Y[0]);
            }
            if(CDTXMania.stage演奏ドラム画面.bDoublePlay && CDTXMania.Tx.Taiko_NamePlate[1] != null)
            {
                CDTXMania.Tx.Taiko_NamePlate[1].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[1], CDTXMania.Skin.Game_Taiko_NamePlate_Y[1]);
            }

            if (CDTXMania.Tx.Taiko_PlayerNumber[0] != null)
            {
                CDTXMania.Tx.Taiko_PlayerNumber[0].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_PlayerNumber_X[0], CDTXMania.Skin.Game_Taiko_PlayerNumber_Y[0]);
            }
            if (CDTXMania.stage演奏ドラム画面.bDoublePlay && CDTXMania.Tx.Taiko_PlayerNumber[1] != null)
            {
                CDTXMania.Tx.Taiko_PlayerNumber[1].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_PlayerNumber_X[1], CDTXMania.Skin.Game_Taiko_PlayerNumber_Y[1]);
            }

            if (CDTXMania.Tx.Taiko_PlateEffect_1 != null && CDTXMania.Tx.Taiko_PlateEffect_2 != null)
            {
                if (this.ctエフェクトスライド.b終了値に達した)
                {
                    this.ctエフェクトスライド.n現在の値 = 0;
                }

                #region 透明度操作
                if (this.ctエフェクトフェード0.b終了値に達した)
                {
                    this.ctエフェクトフェード0.n現在の値 = 0;
                }
                if (this.ctエフェクトフェード1.b終了値に達した)
                {
                    this.ctエフェクトフェード1.n現在の値 = 0;
                }
                if (this.ctエフェクトフェード2.b終了値に達した)
                {
                    this.ctエフェクトフェード2.n現在の値 = 0;
                }
                if (this.ctエフェクトフェード3.b終了値に達した)
                {
                    this.ctエフェクトフェード3.n現在の値 = 0;
                }
                if (this.ctエフェクトフェード4.b終了値に達した)
                {
                    this.ctエフェクトフェード4.n現在の値 = 0;
                }
                if (this.ctエフェクトフェード5.b終了値に達した)
                {
                    this.ctエフェクトフェード5.n現在の値 = 0;
                }
                if (this.ctエフェクトフェード6.b終了値に達した)
                {
                    this.ctエフェクトフェード6.n現在の値 = 0;
                }
                if (this.ctエフェクトフェード7.b終了値に達した)
                {
                    this.ctエフェクトフェード7.n現在の値 = 0;
                }
                #endregion

                int 透明度倍率1 = 15;
                int 透明度倍率2 = 2;

                //this.ctエフェクトフェード.t進行Loop();
                //CDTXMania.Tx.Taiko_PlateEffect_1.n透明度 = (ctエフェクトフェード.n現在の値 * 255 / ctエフェクトフェード.n終了値) / 1;
                #region 一回目のキラキラ

                if (this.ctエフェクト.n現在の値 > 20 && this.ctエフェクト.n現在の値 < 90)
                {
                    this.ctエフェクトフェード0.t進行();
                    if (this.ctエフェクトフェード0.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード0.n現在の値 = 0;
                    }
                    //CDTXMania.Tx.Taiko_PlateEffect_1[7].vc拡大縮小倍率.X = 1.0f;
                    //CDTXMania.Tx.Taiko_PlateEffect_1[7].vc拡大縮小倍率.Y = 1.0f;

                    if (this.ctエフェクト.n現在の値 > 20 && this.ctエフェクト.n現在の値 < 55)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[0].n透明度 = 0 + (this.ctエフェクトフェード0.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 55 && this.ctエフェクト.n現在の値 < 90)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[0].n透明度 = 255 - (this.ctエフェクトフェード0.n現在の値 * 透明度倍率2);
                    }

                    CDTXMania.Tx.Taiko_PlateEffect_1[0].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0], CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] - 5);
                }
                if (this.ctエフェクト.n現在の値 > 60 && this.ctエフェクト.n現在の値 < 130)
                {
                    this.ctエフェクトフェード1.t進行();
                    if (this.ctエフェクトフェード1.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード1.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 60 && this.ctエフェクト.n現在の値 < 95)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[1].n透明度 = 0 + (this.ctエフェクトフェード1.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 95 && this.ctエフェクト.n現在の値 < 130)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[1].n透明度 = 255 - (this.ctエフェクトフェード1.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[1].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 25, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] + 10);
                }
                if (this.ctエフェクト.n現在の値 > 100 && this.ctエフェクト.n現在の値 < 170)
                {
                    this.ctエフェクトフェード2.t進行();
                    if (this.ctエフェクトフェード2.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード2.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 100 && this.ctエフェクト.n現在の値 < 135)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[2].n透明度 = 0 + (this.ctエフェクトフェード2.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 135 && this.ctエフェクト.n現在の値 < 170)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[2].n透明度 = 255 - (this.ctエフェクトフェード2.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[2].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 50, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] - 5);
                }
                if (this.ctエフェクト.n現在の値 > 140 && this.ctエフェクト.n現在の値 < 210)
                {
                    this.ctエフェクトフェード3.t進行();
                    if (this.ctエフェクトフェード3.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード3.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 140 && this.ctエフェクト.n現在の値 < 175)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[3].n透明度 = 0 + (this.ctエフェクトフェード3.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 175 && this.ctエフェクト.n現在の値 < 210)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[3].n透明度 = 255 - (this.ctエフェクトフェード3.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[3].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 75, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] + 10);
                }
                if (this.ctエフェクト.n現在の値 > 180 && this.ctエフェクト.n現在の値 < 250)
                {
                    this.ctエフェクトフェード4.t進行();
                    if (this.ctエフェクトフェード4.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード4.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 180 && this.ctエフェクト.n現在の値 < 215)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[4].n透明度 = 0 + (this.ctエフェクトフェード4.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 215 && this.ctエフェクト.n現在の値 < 250)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[4].n透明度 = 255 - (this.ctエフェクトフェード4.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[4].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 100, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] - 5);
                }
                if (this.ctエフェクト.n現在の値 > 220 && this.ctエフェクト.n現在の値 < 290)
                {
                    this.ctエフェクトフェード5.t進行();
                    if (this.ctエフェクトフェード5.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード5.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 220 && this.ctエフェクト.n現在の値 < 255)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[5].n透明度 = 0 + (this.ctエフェクトフェード5.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 255 && this.ctエフェクト.n現在の値 < 290)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[5].n透明度 = 255 - (this.ctエフェクトフェード5.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[5].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 125, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] + 10);
                }
                if (this.ctエフェクト.n現在の値 > 260 && this.ctエフェクト.n現在の値 < 330)
                {
                    this.ctエフェクトフェード6.t進行();
                    if (this.ctエフェクトフェード6.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード6.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 260 && this.ctエフェクト.n現在の値 < 295)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[6].n透明度 = 0 + (this.ctエフェクトフェード6.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 295 && this.ctエフェクト.n現在の値 < 330)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[6].n透明度 = 255 - (this.ctエフェクトフェード6.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[6].vc拡大縮小倍率.X = 1.0f;
                    CDTXMania.Tx.Taiko_PlateEffect_1[6].vc拡大縮小倍率.Y = 1.0f;
                    CDTXMania.Tx.Taiko_PlateEffect_1[6].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 150, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] - 5);
                }
                #endregion
                #region スラッシュ
                if (this.ctエフェクト.n現在の値 > 330 && this.ctエフェクト.n現在の値 < 510)
                {
                    CDTXMania.Tx.Taiko_PlateEffect_2.n透明度 = 255;
                    this.ctエフェクトスライド.t進行();
                    if (this.ctエフェクトスライド.n現在の値 == 180)
                    {
                        this.ctエフェクトスライド.n現在の値 = 0;
                    }
                    if (this.ctエフェクトスライド.n現在の値 > 130)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_2.n透明度 = 255 - (this.ctエフェクトスライド.n現在の値);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_2.t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] - 20 + this.ctエフェクトスライド.n現在の値, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] + 6);               
                }
                #endregion
                #region 二回目のキラキラ
                if (this.ctエフェクト.n現在の値 > 340 && this.ctエフェクト.n現在の値 < 410)
                {
                    this.ctエフェクトフェード0.t進行();
                    if (this.ctエフェクトフェード0.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード0.n現在の値 = 0;
                    }
                    //CDTXMania.Tx.Taiko_PlateEffect_1[7].vc拡大縮小倍率.X = 1.0f;
                    //CDTXMania.Tx.Taiko_PlateEffect_1[7].vc拡大縮小倍率.Y = 1.0f;

                    if (this.ctエフェクト.n現在の値 > 340 && this.ctエフェクト.n現在の値 < 375)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[0].n透明度 = 0 + (this.ctエフェクトフェード0.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 375 && this.ctエフェクト.n現在の値 < 410)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[0].n透明度 = 255 - (this.ctエフェクトフェード0.n現在の値 * 透明度倍率2);
                    }

                    CDTXMania.Tx.Taiko_PlateEffect_1[0].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0], CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] - 5);
                }
                if (this.ctエフェクト.n現在の値 > 380 && this.ctエフェクト.n現在の値 < 450)
                {
                    this.ctエフェクトフェード1.t進行();
                    if (this.ctエフェクトフェード1.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード1.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 380 && this.ctエフェクト.n現在の値 < 415)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[1].n透明度 = 0 + (this.ctエフェクトフェード1.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 415 && this.ctエフェクト.n現在の値 < 450)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[1].n透明度 = 255 - (this.ctエフェクトフェード1.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[1].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 25, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] + 10);
                }
                if (this.ctエフェクト.n現在の値 > 420 && this.ctエフェクト.n現在の値 < 490)
                {
                    this.ctエフェクトフェード2.t進行();
                    if (this.ctエフェクトフェード2.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード2.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 420 && this.ctエフェクト.n現在の値 < 455)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[2].n透明度 = 0 + (this.ctエフェクトフェード2.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 455 && this.ctエフェクト.n現在の値 < 490)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[2].n透明度 = 255 - (this.ctエフェクトフェード2.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[2].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 50, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] - 5);
                }
                if (this.ctエフェクト.n現在の値 > 460 && this.ctエフェクト.n現在の値 < 530)
                {
                    this.ctエフェクトフェード3.t進行();
                    if (this.ctエフェクトフェード3.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード3.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 460 && this.ctエフェクト.n現在の値 < 495)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[3].n透明度 = 0 + (this.ctエフェクトフェード3.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 495 && this.ctエフェクト.n現在の値 < 530)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[3].n透明度 = 255 - (this.ctエフェクトフェード3.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[3].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 75, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] + 10);
                }
                if (this.ctエフェクト.n現在の値 > 500 && this.ctエフェクト.n現在の値 < 570)
                {
                    this.ctエフェクトフェード4.t進行();
                    if (this.ctエフェクトフェード4.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード4.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 500 && this.ctエフェクト.n現在の値 < 535)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[4].n透明度 = 0 + (this.ctエフェクトフェード4.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 535 && this.ctエフェクト.n現在の値 < 570)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[4].n透明度 = 255 - (this.ctエフェクトフェード4.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[4].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 100, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] - 5);
                }
                if (this.ctエフェクト.n現在の値 > 540 && this.ctエフェクト.n現在の値 < 610)
                {
                    this.ctエフェクトフェード5.t進行();
                    if (this.ctエフェクトフェード5.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード5.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 540 && this.ctエフェクト.n現在の値 < 575)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[5].n透明度 = 0 + (this.ctエフェクトフェード5.n現在の値 * 透明度倍率1);
                    }
                    if (this.ctエフェクト.n現在の値 > 575 && this.ctエフェクト.n現在の値 < 610)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[5].n透明度 = 255 - (this.ctエフェクトフェード5.n現在の値 * 透明度倍率2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[5].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 125, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] + 10);
                }
                if (this.ctエフェクト.n現在の値 > 520 && this.ctエフェクト.n現在の値 < 590)
                {
                    this.ctエフェクトフェード6.t進行();
                    if (this.ctエフェクトフェード6.n現在の値 == 70)
                    {
                        this.ctエフェクトフェード6.n現在の値 = 0;
                    }
                    if (this.ctエフェクト.n現在の値 > 520 && this.ctエフェクト.n現在の値 < 555)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[6].n透明度 = 0 + (this.ctエフェクトフェード6.n現在の値 * 20);
                    }
                    if (this.ctエフェクト.n現在の値 > 555 && this.ctエフェクト.n現在の値 < 590)
                    {
                        CDTXMania.Tx.Taiko_PlateEffect_1[6].n透明度 = 255 - (this.ctエフェクトフェード6.n現在の値 * 2);
                    }
                    CDTXMania.Tx.Taiko_PlateEffect_1[6].vc拡大縮小倍率.X = 2.5f;
                    CDTXMania.Tx.Taiko_PlateEffect_1[6].vc拡大縮小倍率.Y = 2.5f;
                    CDTXMania.Tx.Taiko_PlateEffect_1[6].t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0] + 145, CDTXMania.Skin.Game_Taiko_NamePlate_Y[0] - 25);
                }
                #endregion

            }
            if (CDTXMania.Tx.Taiko_PlateText != null)
            {
                CDTXMania.Tx.Taiko_PlateText.t2D描画(CDTXMania.app.Device, CDTXMania.Skin.Game_Taiko_NamePlate_X[0], CDTXMania.Skin.Game_Taiko_NamePlate_Y[0]);
            }


            //if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.V))
            //{
            //    this.tMtaikoEvent( 0x11, 0, 1 );
            //}
            //if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.N))
            //{
            //    this.tMtaikoEvent( 0x11, 1, 1 );
            //}
            //if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.C))
            //{
            //    this.tMtaikoEvent( 0x12, 0, 1 );
            //}
            //if (CDTXMania.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.M))
            //{
            //    this.tMtaikoEvent( 0x12, 1, 1 );
            //}



            return base.On進行描画();
        }

        public void tMtaikoEvent( int nChannel, int nHand, int nPlayer )
        {
            if( !CDTXMania.ConfigIni.b太鼓パートAutoPlay )
            {
                switch( nChannel )
                {
                    case 0x11:
                    case 0x13:
                    case 0x15:
                    case 0x16:
                    case 0x17:
                        {
                            this.stパッド状態[ 2 + nHand + ( 4 * nPlayer ) ].n明るさ = 8;
                        }
                        break;
                    case 0x12:
                    case 0x14:
                        {
                            this.stパッド状態[ nHand + ( 4 * nPlayer ) ].n明るさ = 8;
                        }
                        break;

                }
            }
            else
            {
                switch( nChannel )
                {
                    case 0x11:
                    case 0x15:
                    case 0x16:
                    case 0x17:
                        {
                            this.stパッド状態[ 2 + nHand + ( 4 * nPlayer ) ].n明るさ = 8;
                        }
                        break;
                            
                    case 0x13:
                        {
                            this.stパッド状態[ 2 + ( 4 * nPlayer ) ].n明るさ = 8;
                            this.stパッド状態[ 3 + ( 4 * nPlayer ) ].n明るさ = 8;
                        }
                        break;

                    case 0x12:
                        {
                            this.stパッド状態[ nHand + ( 4 * nPlayer ) ].n明るさ = 8;
                        }
                        break;

                    case 0x14:
                        {
                            this.stパッド状態[ 0 + ( 4 * nPlayer ) ].n明るさ = 8;
                            this.stパッド状態[ 1 + ( 4 * nPlayer ) ].n明るさ = 8;
                        }
                        break;
                }
            }

        }

        public void tBranchEvent( int Before, int After, int player )
        {
            if( After != Before )
                this.ctレベルアップダウン[ player ] = new CCounter( 0, 1000, 1, CDTXMania.Timer );

            this.After[ player ] = After;
            this.Before[ player ] = Before;
        }


        #region[ private ]
        //-----------------
        //構造体
        [StructLayout(LayoutKind.Sequential)]
        private struct STパッド状態
        {
            public int n明るさ;
        }

        //太鼓
        //private CTexture txMtaiko枠;
        //private CTexture[] txMtaiko下敷き = new CTexture[ 4 ];

        //private CTexture tx太鼓_土台;
        //private CTexture tx太鼓_面L;
        //private CTexture tx太鼓_ふちL;
        //private CTexture tx太鼓_面R;
        //private CTexture tx太鼓_ふちR;

        private STパッド状態[] stパッド状態 = new STパッド状態[ 4 * 4 ];
        private long nフラッシュ制御タイマ;

        //private CTexture[] txコースシンボル = new CTexture[ 6 ];
        private string[] strCourseSymbolFileName;

        //オプション
        private CTexture txオプションパネル_HS;
        private CTexture txオプションパネル_RANMIR;
        private CTexture txオプションパネル_特殊;
        private int nHS;

        //ネームプレート
        //private CTexture txネームプレート;
        //private CTexture txネームプレート2P;

        //譜面分岐
        private CCounter[] ctレベルアップダウン;
        private int[] After;
        private int[] Before;
        private CCounter ctエフェクト;
        private CCounter ctエフェクトフェード0;
        private CCounter ctエフェクトフェード1;
        private CCounter ctエフェクトフェード2;
        private CCounter ctエフェクトフェード3;
        private CCounter ctエフェクトフェード4;
        private CCounter ctエフェクトフェード5;
        private CCounter ctエフェクトフェード6;
        private CCounter ctエフェクトフェード7;
        private CCounter ctエフェクトスライド;
        //private CTexture txレベルアップ;
        //private CTexture txレベルダウン;
        //-----------------
        #endregion

    }
}
　
