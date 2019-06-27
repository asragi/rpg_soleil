# FontImage
基底クラス[ImageBase](ImageBase.md)に基本的な使用方法が記述されている．

## Field

### Text
表示内容を設定する．デフォルトで `""` なので設定しないと何も表示されない．

### OutLineColor
縁取りの色を設定する．

## Method

### ActivateOutLine(int diff)
縁取り `OutLine` を有効化する．

#### diff
縁取りの大きさを指定する．
現状，サイズ2以上では表示が乱れるので非推奨．
