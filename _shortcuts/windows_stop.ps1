

Write-Host "Stopping the application...";

try {
	docker-compose --project-name "lactalis" down;

	do {
		Start-Sleep 1;
	} Until((docker-compose --project-name "lactalis" ps | Measure-Object -Line).Lines -lt 3)

	Write-Host "=================================";
	Write-Host "Application stopped successfully!";
	Write-Host "=================================";
} catch {
	Write-Host "Exception Message: $($_.Exception.Message)" -ForegroundColor Red
} finally {
	Read-Host "Press enter to close window..."
}