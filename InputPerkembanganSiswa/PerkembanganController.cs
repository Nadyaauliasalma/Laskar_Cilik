using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/[controller]")]
public class PerkembanganController : ControllerBase
{
    private readonly PerkembanganService service;

    public PerkembanganController(PerkembanganService service)
    {
        this.service = service;
    }

    [HttpPost]
    public IActionResult Tambah([FromBody] InputPerkembanganDto dto)
    {
        try
        {
            var hasil = service.Post(dto);
            return Ok(hasil);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var data = service.GetById(id);
        return data != null ? Ok(data) : NotFound("Data tidak ditemukan");
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] InputPerkembanganDto dto)
    {
        try
        {
            bool berhasil = service.Update(id, dto);
            return berhasil ? Ok("Data berhasil diperbarui") : NotFound("Data tidak ditemukan");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        bool berhasil = service.Delete(id);
        return berhasil ? Ok("Data berhasil dihapus") : NotFound("Data tidak ditemukan");
    }
}

