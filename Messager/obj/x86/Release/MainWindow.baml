   M S B A M L   `   `   `  ���� C  ?Messager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MessagerMessager      Messager.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     (localclr-namespace:Messager     -     � Title$  Messager��$	��412��$	��660��$	��660�� � 	Resources   Messager.OnlineConverter   OnlineConverter"  Messager.NewMessageConverter  NewMessageConverter#  Messager.TextMessageConverter  TextMessageConverter  OnlineStyle �� 
TargetType*  $System.Windows.Controls.DataGridCell �� Property �� Value �� 	Converter  NewMessageStatus  	TextStyle  TextStyleWithoutHorizontal��%�  &	        &	      &	 
     &	      7   &	 Z     7  &	 �     &	 
          �� "  .���  � �� �� *Online+8   ��  � ��
 �   ��� "  .���  � �� �� *LastMessage.Readed+8   ��  � ��
 �   ��� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$
 Wrap=��� " ��.���  � ��$ Center=���  � ��$
 Wrap=�.���  � ColumnDefinitions �� $��53 ��� $	��122 ��� $��69 ��� $	��122 ��� $��36 ��� $��85 ��� $	��150 � � RowDefinitions �� $��20 ��� $��25 ��� $��8 ��� $��* �.+�&   System.Windows.Controls.DataGrid  -   $��	dataGrid1��   AutoGenerateColumns .  $��3��$��7������ 	 �� Source$	 Friends��
   Columns
 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Photo��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50    CellTemplate �� .R��� $��50��$��50������ *Photo+0	  *System.Windows.Controls.DataGridTextColumn	  $ NickName��$	 100    	CellStyle# [ 1
  +System.Windows.Controls.DataGridBoundColumn 
  ElementStyle# [    
IsReadOnly .  
  Binding �� *
NickName+	  $	 Age��$ 40 # [  .  �� *Age+  $
 Flag��$ 40  �� .R��� $��30��$��30������ *Country_Flag+	  $ Country��$ 90 # [  .  �� *	Country+	  $
 City��$ 80 # [  .  �� *City+	  $ 
LastMesage��$	 150 # [ # [  .  �� *LastMessage+# [   $ Messages�� �� .R��� $��MessageHistory��-   $��Messages���� -   $��Login��$��1��$��1�� �� MaxLines$ 1��$��0���� -   $��
AuthButton��$��Enter��$��5��$��1��$��2��G� -   $��Password��$��1��$��3��$��25��$��1���� $��Login��$��1��$��0��$��4q��� $��Password��$��2��$��1��$��4q�