<?xml version="1.0" ?>
<xsl:stylesheet version="1.0"     xmlns:xsl="http://www.w3.org/1999/XSL/Transform"     xmlns:wix="http://schemas.microsoft.com/wix/2006/wi">
    <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
    
    <!-- Adds all by default -->
    <xsl:template match="@*|*">
        <xsl:copy>
            <xsl:apply-templates select="@*" />
            <xsl:apply-templates select="*" />
        </xsl:copy>
    </xsl:template>

    <!-- Except those -->
    <!-- http://stackoverflow.com/questions/8737356/how-can-i-exclude-svn-files-from-harvesting-with-heat-wix -->
    <!-- http://habrahabr.ru/post/122038/ -->
    <xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('.vshost.exe') + 1) = '.vshost.exe']" />
    <xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('.vshost.exe.config') + 1) = '.vshost.exe.config']" />
    <xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('.vshost.exe.manifest') + 1) = '.vshost.exe.manifest']" />
    <xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('.noReferenceAssembly') + 1) = '.noReferenceAssembly']" />
    <xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('.InstallLog') + 1) = '.InstallLog']" />
    <xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('.InstallState') + 1) = '.InstallState']" />
    <xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('.pdb') + 1) = '.pdb']" />	
	<xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('.log') + 1) = '.log']" />
	<xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('Infotecs.Attika.AttikaConsoleHost.exe') + 1) = 'Infotecs.Attika.AttikaConsoleHost.exe']" />
	<xsl:template match="wix:Fragment/wix:ComponentGroup/wix:Component[substring(wix:File/@Source, string-length(wix:File/@Source) - string-length('Infotecs.Attika.AttikaGui.exe') + 1) = 'Infotecs.Attika.AttikaGui.exe']" />
    <xsl:template match="wix:Fragment[wix:DirectoryRef]" />
  
</xsl:stylesheet>