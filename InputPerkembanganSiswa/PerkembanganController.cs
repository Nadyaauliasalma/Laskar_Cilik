using Microsoft.AspNetCore.Mvc;
using System;

namespace PerkembanganSiswa
{
    [ApiController]
    [Route("api/perkembangan")] // 👈 lebih eksplisit dan stabil
    public class PerkembanganController : ControllerBase
    {
        private readonly PerkembanganService service;

        public PerkembanganController(PerkembanganService service)
        {
            this.service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Tambah([FromBody] InputPerkembanganDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var hasil = service.Post(dto);
                return CreatedAtAction(nameof(GetById), new { id = hasil.Id }, hasil);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(service.GetAll());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var data = service.GetById(id);
            return data != null ? Ok(data) : NotFound(new { error = "Data tidak ditemukan" });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] InputPerkembanganDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                bool berhasil = service.Update(id, dto);
                return berhasil ? Ok("Data berhasil diperbarui") : NotFound("Data tidak ditemukan");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            bool berhasil = service.Delete(id);
            return berhasil ? Ok("Data berhasil dihapus") : NotFound("Data tidak ditemukan");
        }
    }
}
