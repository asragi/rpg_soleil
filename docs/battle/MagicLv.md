# MagicLv
魔法には10種類の系統([MagicCategory](/docs/battle/MagicCategory.md))がある．キャラクターはそれぞれの系統ごとにLvを持つ．Lvは[0, 9]の値を取る．

## Field

## Method

### void AddExp(int val, MagicCategory target)
`target`で指定された術系統に`val`の経験値を与える．加算時にLv上昇閾値を越えた場合にスキル獲得処理を行う．

### bool IsLearned(MagicCategory target)
引数で指定された術系統を習得しているかどうかを返す．

### int GetLv(MagicCategory target)
引数で指定された術系統の習得レベルを返す．
