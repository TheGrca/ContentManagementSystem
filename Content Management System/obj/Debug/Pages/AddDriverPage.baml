   M S B A M L   `   `   `  ����T  PContent Management System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Content_Management_System.PagesContent_Management_System.Pages  5      3    -Content_Management_System.Pages.AddDriverPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TFontAwesome5.Net, Version=2.1.11.0, Culture=neutral, PublicKeyToken=9cfaf01297a008f8/fa5%http://schemas.fontawesome.com/icons/  5      9local-clr-namespace:Content_Management_System.Pages  5        N� Title$  AddDriverPage��5
      .��5      ��  �� CornerRadius$ 20��6   $	��650��6   $	��480��6+   ���#8D919C._�5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��1.3* �6    �� 5      $��* �6    �� 5      $��6* �6    �� 5      $��* �6    .+�5      �� -   5         ��Name$ 
BackButton��5      $��30��6)   $	��100��65    � Cursor$
 Hand��6A   $
��Left=�6O   $��20 5 0 0q�6j      MyButton#��[  6�    � ToolTip$" Return to the previous page.��6�   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5        FontAwesome5.SvgAwesome  $��13��5      $��Center=�5      $��Center=�5         Icon$ Solid_ArrowLeft=�5       	   
Foreground
	 �����$��0 0 5 0q�5"      �� 5#      $��3,0,0,0q�5$      $��Center=�5%      $
��BACK��5&      5'      5(      �� 5)      $��2��6   $��ADD A DRIVER��6,   $��DemiBold"�6@   $��Center=�6V   $��Center=�6s   $��30(�6�   c� 5+      $��1��6   $��2��6#   $	��630��67   $��30��6C   $��Center=�6O   .@�5,      d� -   5        
 d�Name$
 BoldToggleButton��5,      $��40 0 0 0q�69   $ ��EditingCommands.ToggleBold��6K   $" Make the selected text bold.��6p   .��5-        $ 
Solid_Bold=�6%   
	 �   �67   5.      �� 5/      $��	10 0 10 0q�6   d� 50      -   5        $
 ItalicToggleButton��50      $"��EditingCommands.ToggleItalic��6;   $$ Make the selected text italic.��6b   .��51        $ Solid_Italic=�6%   
	 �   �69   52      �� 53      $��	10 0 10 0q�6   d� 54      -   5        $
 UnderlineToggleButton��54      $%��EditingCommands.ToggleUnderline��6>   $( "Make the selected text underlined.��6h   .��55        $ Solid_Underline=�6%   
	 �   �6<   56      �� 57      $��	10 0 10 0q�6   �� 58      -   5         ��Name$ FontFamilyComboBox��58      $	��150��67     �� IsTextSearchCaseSensitive .   �� IsTextSearchEnabled . $ Change the font family.��6�   �� 59      $��	10 0 10 0q�6   �� 5:      -   5        $ FontSizeComboBox��5:      $��50��65    . $ Change the font size.��6�   �� 5;      $��	10 0 10 0q�6   �� 5<      -   5         ��Name$ ColorPreviewRectangle��5<      $��20��6;   $��20��6G   $��0 0 5 0q�6R   �� 5=      -   5        $ FontColorComboBox��5=      $	��100��66    .   . $ Change the font color.��6�   5>      �� 5@      $��2��6   .+�5A      �� $	��300��6   $��30��6$   $��DRIVER NAME:��60   $��15(�6G   $��SemiBold"�6U   �� 5B      -	   5         ��Name$ DriverNameTextBox��5B      $	��300��65   $��30��6A   $��Center=�6M   �� 5C      $ 
Horizontal=�6   $	��300��66   .+�5D      �� -
   5         ��Name$ DriverNameErrorLabel��5D      $	��250��6:   $��30��6F   $	��Top=�6R   
���  ��6j   5E      5F      �� 5H      $��	0 100 0 0q�6   $��2��60   .+�5I      �� $	��300��6   $��30��6(   $��DRIVER NUMBER:��64   $��15(�6M   $��SemiBold"�6[   $��0 0 0 5q�6q   �� 5J      -   5        $ DriverNumberTextBox��5J      $	��300��6;   $��30��6G   $��Center=�6S   �� 5K      -   5        $ DriverNumberErrorLabel��5K      $	��300��6<   $��30��6H   
���  ��6T   5L      �� 5N      $ 
Horizontal=�6   $��2��62   $��	0 200 0 0q�6?   .+�5P      �� $��2��6   $	��221��6+   .+�5Q      �� $	��130��6   $��30��6(   $��DRIVER PICTURE:��64   $
��Left=�6N   $��15(�6i   $��SemiBold"�6w   $��15 0 0 0q�6�   �� 5R      -   5        $ SelectPictureButton��5R      $	��150��6:   $��40��6F   $
 Hand��6R   #��[  6�   $! Select a picture to upload.��6�   $��0 5 40 0q�6�   .��5S      �� $ 
Horizontal=�6%   .+�5T        $��13��5U      $��Center=�5V      $��Center=�5W      $ Solid_Camera=�5X      
	 �����$��0 0 5 0q�5Z      �� 5[      $��3,0,0,0q�5\      $��Center=�5]      $��SELECT PICTURE��5^      5_      5`      �� 5a      -   5        $ DriverPictureErrorLabel��5a      $	��209��6=   $��30��6I   $��10 0 0 0q�6U   
���  ��6g   5b      �� 5c      $��80��6   $��80��6(    �� Stretch$ Uniform=�64   ��5d      �� 5e      -   5        PreviewPictureBrush��5e   %   5g      5h      �� 5j      $��DRIVER DESCRIPTION:��6   $��2��62   $��1��6?   $��15(�6O   $��SemiBold"�6]   $��5 0 0 0q�6s   �� 5k      -   5         ��Name$" DriverDescriptionRichTextBox��5k      $��1��6@   $��2��6P   $��10 30 10 40q�6]   6r   �� 5l      -   5        $! DescriptionRichTextBoxError��5l      $��1��69   $��2��6I   $��Bottom=�6V   $��30��6q   $	��250��6}   $��15 0 0 7q�6�   $
��Left=�6�   
���  ��6�   �� 5m      -   5         ��Name$ CharacterCounterStatusBar��5m      $��35��6;   $��35��6F   $��1��6R   $��2��6b   $��Bottom=�6o   $��Right=�6�   $��0 0 11 6q�6�   
�������.@�5n      �� .��5o      �� -   5        $ CharacterCounterLabel��5o      $��10(�6;   5p      5q      �� 5s      -   5        $ AddDriverButton��5s      $��5��6.   $��2��6;   $
 Hand��6O   $��50��6]   $	��200��6i   $��Center=�6u   $��Center=�6�   #��[  6�   $) #Add the driver to Mercedes F1 team.��6�   .��5t      �� $ 
Horizontal=�6   .+�5u        $��13��5v      $��Center=�5w      $��Center=�5x      $ 
Solid_Plus=�5y      
	 �����$��0 0 5 0q�5{      �� 5|      $��3,0,0,0q�5}      $��Center=�5~      $��
ADD DRIVER��5      5�      5�      5�      5�      5�      